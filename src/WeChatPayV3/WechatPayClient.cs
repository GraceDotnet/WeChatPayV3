using MQTTnet.Diagnostics;
using Newtonsoft.Json;
using SDK.WeChatPayV3.Interface;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeChatPayV3.Extension;
using WeChatPayV3.Infrastructure;
using WeChatPayV3.Interface;
using WeChatPayV3.Model;
using WeChatPayV3.Model.Base;
using WeChatPayV3.Notify;
using WeChatPayV3.Request;
using WeChatPayV3.Response;

namespace WeChatPayV3
{
    public sealed class WechatPayClient : IWeChatPayClient
    {
        IClientLogger logger;

        public WechatPayClient(IClientLogger logger)
        {
            this.logger = logger ?? new ClientNullLogger();
        }

        public WechatPayClient()
            : this(null)
        {
        }

        /// <summary>
        /// 发起apiv3接口调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public T ExecuteRequest<T>(IWechatPayRequestSDK<T> request, WechatOptions options) where T : WechatPayBaseResponse
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client.ExecuteResponse(request, options);

                if (request.ValidateResponse)
                {
                    try
                    {
                        ValidateSignAsync(result.headers, result.responseContent, options);
                    }
                    catch (Exception ex)
                    {
                        result.sdkResponse.Code = "400";
                        result.sdkResponse.Message = ex.Message;
                    }
                }

                return result.sdkResponse;
            }
        }

        public SDKAtivator ExecuteSDK(IWechatPaySDK ativator, WechatOptions options)
        {
            var resp = new SDKAtivator
            {
                AppId = options.AppId,

                TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),

                NonceStr = Guid.NewGuid().ToString("N"),

                Package = ativator.Package
            };

            string message = $"{resp.AppId}\n{resp.TimeStamp}\n{resp.NonceStr}\n{resp.Package}\n";

            using (RSA rsa = options.Certificate.GetRSAPrivateKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                resp.PaySign = Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }

            return resp;
        }

        public T ExecuteNofity<T>(WechatPayHeader header, WechatNotificationPayload<T> notification, WechatOptions options) where T : WechatPayNotification
        {
            try
            {
                ValidateSignAsync(header, JsonConvert.SerializeObject(notification, Formatting.None), options);

                byte[] decryptRawContent;

                switch (notification?.EncryptInfo.Algorithm)
                {
                    case "AEAD_AES_256_GCM":
                        {
                            decryptRawContent = notification.EncryptInfo.Decrypt(options.APISecret);
                        }
                        break;
                    default:
                        throw new Exception("Unsupported Encrypt Algorithm!");

                }

                notification.WechatPayNotification = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(decryptRawContent));


            }
            catch (Exception)
            {
                throw;
            }

            return notification.WechatPayNotification;
        }

        /// <summary>
        /// 验证应答签名
        /// </summary>
        /// <param name="header"></param>
        /// <param name="responseContent"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private void ValidateSignAsync(WechatPayHeader header, string responseContent, WechatOptions options)
        {
            if (string.IsNullOrWhiteSpace(header?.Nonce)
                || string.IsNullOrWhiteSpace(header.SerialNo)
                || string.IsNullOrWhiteSpace(header.Signature)
                || string.IsNullOrWhiteSpace(header.TimeStamp))

                throw new ArgumentException();

            var certificate = GetPlatformCertificateAsync(header.SerialNo, options);

            if (certificate == null)
            {
                throw new Exception("Can't Get PLATFORM CERTIFICATE");
            }

            string message = $"{header.TimeStamp}\n{header.Nonce}\n{responseContent}\n";

            using (var rsa = certificate.GetRSAPublicKey())
            {
                if (!rsa.VerifyData(Encoding.UTF8.GetBytes(message), Convert.FromBase64String(header.Signature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))

                    throw new Exception("Validate Sinature Failed!");
            }
        }

        /// <summary>
        /// 获取平台证书
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private X509Certificate2 GetPlatformCertificateAsync(string serialNo, WechatOptions options)
        {
            var certificate = PlateformCertificateManager.CertificateManager[serialNo];

            if (certificate != null)
            {
                return certificate;
            }

            var response = ExecuteRequest(new WechatPayPlatformCertificateRequest(), options);

            if (response.Data.Equals(null) || response.Data.Length == 0)
            {
                throw new Exception("GET PLATFORM CERTIFICATE FAILED!");
            }

            foreach (var item in response.Data)
            {
                switch (item.EncryptInfo.Algorithm)
                {
                    case "AEAD_AES_256_GCM":
                        {
                            PlateformCertificateManager.CertificateManager[serialNo] = new X509Certificate2(item.EncryptInfo.Decrypt(options.APISecret));
                        }
                        break;
                    default:
                        throw new Exception("Unsupported Encrypt Algorithm!");
                }
            }

            return PlateformCertificateManager.CertificateManager[serialNo];
        }
    }
}

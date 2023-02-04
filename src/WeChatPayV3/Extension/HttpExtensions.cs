using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeChatPayV3.Interface;
using WeChatPayV3.Model.Base;
using WeChatPayV3.Response;

namespace WeChatPayV3.Extension
{
    internal static class HttpExtensions
    {
        private static readonly JsonSerializerSettings jsonSerializerOptions = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static ExecuteResult<T> ExecuteResponse<T>(this HttpClient client, IWechatPayRequestSDK<T> request, WechatOptions options)
            where T : WechatPayBaseResponse
        {
           
            string method = request.RequestMethod, url = request.RequestUrl, body = default, token;

            switch (method)
            {
                case "GET":
                    token = BuildToken(url, method, null, options);
                    break;
                case "POST":
                case "PUT":
                case "PATCH":
                    body = JsonConvert.SerializeObject(request, Formatting.None);
                    token = BuildToken(url, method, body, options);
                    break;
                default:
                    throw new ArgumentException();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WECHATPAY2-SHA256-RSA2048", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("tgj", "2.0")));

            HttpResponseMessage resp = default;

            switch (method)
            {
                case "GET":
                    resp = client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();
                    break;
                case "POST":
                case "PUT":
                case "PATCH":
                    resp = client.SendAsync(new HttpRequestMessage()
                    {
                        Method = new HttpMethod(method),
                        Content = new StringContent(body, Encoding.UTF8, "application/json"),
                        RequestUri = new Uri(url)
                    }).ConfigureAwait(false).GetAwaiter().GetResult();
                    break;
                default:
                    throw new Exception($"不支持的请求方法：{method}");
            }

            using (var respContent = resp.Content)
            {
                string content = respContent.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                if (request.ValidateResponse)
                {
                    return new ExecuteResult<T>()
                    {
                        headers = GetWeChatPayHeadersFromResponse(resp.Headers),
                        responseContent = content,
                        sdkResponse = JsonConvert.DeserializeObject<T>(content, jsonSerializerOptions)
                    };
                }
                else
                {
                    return new ExecuteResult<T>()
                    {
                        sdkResponse = JsonConvert.DeserializeObject<T>(content, jsonSerializerOptions)
                    };
                }
            }
        }

        /// <summary>
        /// 构造签名头
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static string BuildToken(string url, string method, string body, WechatOptions options)
        {
            var uri = new Uri(url).PathAndQuery;

            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            string nonce = Guid.NewGuid().ToString("N");

            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";

            string signature;

            using (RSA rsa = options.Certificate.GetRSAPrivateKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);

                signature = Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }

            return $"mchid=\"{options.MerchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{options.Certificate.SerialNumber}\",signature=\"{signature}\"";
        }

        /// <summary>
        /// 获取微信响应头
        /// </summary>
        /// <param name="reponseHeader"></param>
        /// <returns></returns>
        private static WechatPayHeader GetWeChatPayHeadersFromResponse(HttpResponseHeaders reponseHeader)
        {
            var headers = new WechatPayHeader();

            IEnumerable<string> value;

            if (reponseHeader.TryGetValues("Wechatpay-Nonce", out value))
            {
                headers.Nonce = value.First();
            }

            if (reponseHeader.TryGetValues("Wechatpay-Signature", out value))
            {
                headers.Signature = value.First();
            }

            if (reponseHeader.TryGetValues("Wechatpay-Timestamp", out value))
            {
                headers.TimeStamp = value.First();
            }

            if (reponseHeader.TryGetValues("Wechatpay-Serial", out value))
            {
                headers.SerialNo = value.First();
            }

            return headers;
        }
    }
}

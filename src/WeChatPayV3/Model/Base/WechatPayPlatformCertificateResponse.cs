using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using WeChatPayV3.Utility;
using WeChatPayV3.SDK.Model;
using WeChatPayV3.Model.Base;

namespace WeChatPayV3.Response
{

    /// <summary>
    /// 获取平台证书列表
    /// https://wechatpay-api.gitbook.io/wechatpay-api-v3/jie-kou-wen-dang/ping-tai-zheng-shu
    /// </summary>
    public sealed class WechatPayPlatformCertificateResponse : WechatPayBaseResponse
    {
        /// <summary>
        /// 证书列表
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public PlatformCertificate[] Data { get; set; }
    }

    #region 证书信息
    /// <summary>
    /// 平台响应证书详情
    /// </summary>
    public class PlatformCertificate
    {
        [JsonProperty(PropertyName = "serial_no")]
        public string SerialNo { get; set; }

        [JsonProperty(PropertyName = "effective_time")]

        public DateTimeOffset EffectiveTime { get; set; }


        [JsonProperty(PropertyName = "expire_time")]
        public DateTimeOffset ExpireTime { get; set; }


        [JsonProperty(PropertyName = "encrypt_certificate")]
        public EncryptInfo EncryptInfo { get; set; }
    }


    #endregion

}

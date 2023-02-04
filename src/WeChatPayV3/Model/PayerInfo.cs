﻿

using Newtonsoft.Json;

namespace WeChatPayV3.Model
{
    /// <summary>
    /// 支付者信息
    /// </summary>
    public class PayerInfo
    {


        [JsonProperty(PropertyName ="openid")]
        /// <summary>
        /// 用户在直连商户appid下的唯一标识。
        /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
        /// </summary>
        public string OpenId { get; set; }
    }
}

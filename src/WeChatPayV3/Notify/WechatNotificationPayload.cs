using Newtonsoft.Json;
using WeChatPayV3.Notify;
using WeChatPayV3.SDK.Model;

namespace WeChatPayV3.Response
{
    /// <summary>
    /// 通知报文
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/wxpay/pay/transactions/chapter3_11.shtml
    /// </summary>
    public class WechatNotificationPayload<T> where T : WechatPayNotification
    {
        /// <summary>
        /// 通知ID
        /// 示例值：EV-2018022511223320873
        /// </summary>
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }

        /// <summary>
        /// 通知创建的时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2015-05-20T13:29:35+08:00
        /// </summary>
        [JsonProperty(PropertyName ="create_time")]
        public string CreateTime { get; set; }

        /// <summary>
        /// 通知的类型，支付成功通知的类型为TRANSACTION.SUCCESS
        /// 示例值：TRANSACTION.SUCCESS
        /// </summary>
        [JsonProperty(PropertyName ="event_type")]
        public string EventType { get; set; }

        /// <summary>
        /// 通知的资源数据类型，支付成功通知为encrypt-resource
        /// 示例值：encrypt-resource
        /// </summary>
        [JsonProperty(PropertyName ="resource_type")]
        public string ResourceType { get; set; }

        /// <summary>
        /// 通知加密信息
        /// </summary>
        [JsonProperty(PropertyName ="resource")]
        public EncryptInfo EncryptInfo { get; set; }

        /// <summary>
        /// 回调摘要
        /// 示例值：支付成功
        /// </summary>
        [JsonProperty(PropertyName ="summary")]
        public string Summary { get; set; }

        [JsonIgnore]
        /// <summary>
        /// 解密后的通知信息
        /// </summary>
        public T WechatPayNotification { get; set; }
    }

}

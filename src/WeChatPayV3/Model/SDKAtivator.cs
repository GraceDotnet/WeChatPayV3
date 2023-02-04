using Newtonsoft.Json;

namespace ZhiFou.WeChatPayV3.Model
{
    /// <summary>
    /// 调起
    /// </summary>
    public class SDKAtivator
    {

        [JsonProperty(PropertyName = "appId")]
        public string AppId { get; set; }

        [JsonProperty(PropertyName = "timeStamp")]
        public long TimeStamp { get; set; }

        [JsonProperty(PropertyName = "nonceStr")]
        public string NonceStr { get; set; }

        [JsonProperty(PropertyName = "package")]
        public string Package { get; set; }

        [JsonProperty(PropertyName = "signType")]
        public string SignType { get; private set; } = "RSA";

        [JsonProperty(PropertyName = "paySign")]
        public string PaySign { get; set; }

    }
}

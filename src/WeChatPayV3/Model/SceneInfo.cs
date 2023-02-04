using Newtonsoft.Json;

namespace ZhiFou.WeChatPayV3.Model
{
    /// <summary>
    /// 场景信息
    /// </summary>
    public class SceneInfo
    {
        /// <summary>
        /// 商户端设备号
        /// </summary>
        [JsonProperty(PropertyName = "device_id")]
        public string DeviceId { get; set; }
    }
}

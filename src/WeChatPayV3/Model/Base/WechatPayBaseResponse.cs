

using Newtonsoft.Json;

namespace ZhiFou.WeChatPayV3.Model.Base
{

    /// <summary>
    /// 响应基类
    /// </summary>
    public abstract class WechatPayBaseResponse
    {
        #region 通用错误信息

        /// <summary>
        /// 详细错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// 错误描述
        /// 使用易理解的文字表示错误的原因。
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = "OK";


        /// <summary>
        /// 错误详情
        /// </summary>
        [JsonProperty(PropertyName = "detail")]
        public ErrorInfo Detail { get; set; }

        #endregion

    }

    /// <summary>
    /// 具体错误原因
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// 指示错误参数的位置。
        /// 当错误参数位于请求body的JSON时，填写指向参数的JSON 
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// 错误的值
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// 具体错误原因
        /// </summary>
        [JsonProperty(PropertyName = "issue")]
        public string Issue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }
}

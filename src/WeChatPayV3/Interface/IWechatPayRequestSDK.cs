using Newtonsoft.Json;
using ZhiFou.WeChatPayV3.Model.Base;

namespace ZhiFou.WeChatPayV3.Interface
{
    public interface IWechatPayRequestSDK<T> where T : WechatPayBaseResponse
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        [JsonIgnore]
        string RequestUrl { get; }

        /// <summary>
        /// 请求方法
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        string RequestMethod { get; }

        /// <summary>
        /// 是否验证回调应答签名
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        bool ValidateResponse { get; }
    }
}

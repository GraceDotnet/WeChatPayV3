using System.Threading.Tasks;
using WeChatPayV3.Model;
using WeChatPayV3.Model.Base;
using WeChatPayV3.Notify;
using WeChatPayV3.Response;

namespace WeChatPayV3.Interface
{
    public interface IWeChatPayClient
    {

        /// <summary>
        /// 执行HTTP Request请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        T ExecuteRequest<T>(IWechatPayRequestSDK<T> request, WechatOptions options) where T : WechatPayBaseResponse;

        /// <summary>
        /// 执行SDK
        /// </summary>
        /// <param name="ativator"></param>
        /// <param name="options"></param>
        /// <returns></returns>

        SDKAtivator ExecuteSDK(IWechatPaySDK ativator, WechatOptions options);



        /// <summary>
        /// 处理通知对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header"></param>
        /// <param name="notification"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        T ExecuteNofity<T>(WechatPayHeader header, WechatNotificationPayload<T> notification, WechatOptions options) where T : WechatPayNotification;
    }
}

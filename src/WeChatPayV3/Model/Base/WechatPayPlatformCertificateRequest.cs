using WeChatPayV3.Interface;
using WeChatPayV3.Response;

namespace WeChatPayV3.Request
{

    /// <summary>
    /// 获取平台证书列表
    /// https://wechatpay-api.gitbook.io/wechatpay-api-v3/jie-kou-wen-dang/ping-tai-zheng-shu
    /// </summary>
    internal class WechatPayPlatformCertificateRequest : IWechatPayRequestSDK<WechatPayPlatformCertificateResponse>
    {
        public  string RequestUrl=> "https://api.mch.weixin.qq.com/v3/certificates";
    
        public  string RequestMethod=> "GET";

        public bool ValidateResponse => false;
    }
}

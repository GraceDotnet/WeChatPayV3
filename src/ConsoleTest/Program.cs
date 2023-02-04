using ConsoleTest.Models;
using System;
using ZhiFou.WeChatPayV3;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 接口调用示例
            // https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml
            var client = new WechatPayClient();
            var res = client.ExecuteRequest(new TransactionsJsapiRequest
            {
                mchid = "161XXXXXXX",
                out_trade_no = "1217752501201407033233368318",
                appid = "wxdace645e0bc2cXXX",
                description = "Image形象店-深圳腾大-QQ公仔",
                notify_url = "https://www.weixin.qq.com/wxpay/pay.php",
                amount = new Amount
                {
                    currency = "CNY",
                    total = 1
                },
                payer = new Payer { openid = "o4GgauInH_RCEdvrrNGrntXDuXXX" }
            }, new WechatOptions("wxdace645e0bc2cXXX", "161XXXXXXX", "cert.p12", "752c744226e5dadad099b406148c9"));

            Console.WriteLine("OK");
            Console.ReadLine();
        }
    }
}

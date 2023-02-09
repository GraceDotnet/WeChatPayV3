using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFou.WeChatPayV3.Interface;
using ZhiFou.WeChatPayV3.Model;

namespace ConsoleTest.Models
{
    public class TransactionsJsapiRequest : IWechatPayRequestSDK<TransactionsJsapiResponse>
    {
        public string mchid { get; set; }

        public string out_trade_no { get; set; }

        public string appid { get; set; }

        public string description { get; set; }

        public string notify_url { get; set; }

        public Amount amount { get; set; }

        public Payer payer { get; set; }

        public string RequestUrl => "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";

        public string RequestMethod => "POST";

        public bool ValidateResponse => false;
    }

    public class Payer
    {
        public string openid { get; set; }
    }

}

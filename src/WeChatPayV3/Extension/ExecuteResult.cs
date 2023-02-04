using System;
using System.Collections.Generic;
using System.Text;
using ZhiFou.WeChatPayV3.Model;

namespace ZhiFou.WeChatPayV3.Extension
{
    public class ExecuteResult<T>
    {
        public WechatPayHeader headers { get; set; }

        public string responseContent { get; set; }

        public T sdkResponse { get; set; }
    }
}

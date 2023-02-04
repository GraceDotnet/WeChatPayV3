using System;
using System.Collections.Generic;
using System.Text;
using WeChatPayV3.Response;

namespace WeChatPayV3.Extension
{
    public class ExecuteResult<T>
    {
        public WechatPayHeader headers { get; set; }

        public string responseContent { get; set; }

        public T sdkResponse { get; set; }
    }
}

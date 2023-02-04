using System;
using ZhiFou.WeChatPayV3.Interface;

namespace ZhiFou.WeChatPayV3.Model
{
    public sealed class ClientNullLogger : IClientLogger
    {
        public ClientNullLogger()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; }

        public void Publish(ClientLoggerLevel logLevel, string source, string message, object[] parameters, Exception exception)
        {
        }
    }
}
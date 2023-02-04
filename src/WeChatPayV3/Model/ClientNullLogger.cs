using SDK.WeChatPayV3.Interface;
using System;

namespace MQTTnet.Diagnostics
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
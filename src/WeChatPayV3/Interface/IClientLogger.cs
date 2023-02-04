using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFou.WeChatPayV3.Interface
{
    public interface IClientLogger
    {
        bool IsEnabled { get; }

        void Publish(ClientLoggerLevel logLevel, string source, string message, object[] parameters, Exception exception);
    }

    public enum ClientLoggerLevel
    {
        Verbose,

        Info,

        Warning,

        Error
    }
}

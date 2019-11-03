using System;
using Microsoft.Extensions.Logging;

namespace Wally.RomMaster.Domain.Interfaces
{
    public interface IDebuggerService
    {
        ILoggerProvider LoggerProvider { get; }

        event EventHandler<string> MessageReceived;
    }
}

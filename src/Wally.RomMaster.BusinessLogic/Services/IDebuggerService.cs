using System;
using Microsoft.Extensions.Logging;

namespace Wally.RomMaster.BusinessLogic.Services
{
    public interface IDebuggerService
    {
        ILoggerProvider LoggerProvider { get; }

        event EventHandler MessageReceived;
    }
}

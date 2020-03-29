using System;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;

namespace Wally.RomMaster.Domain.Interfaces
{
    public interface IDebuggerService
    {
        ILoggerProvider LoggerProvider { get; }

        // event EventHandler<string> MessageReceived;

        ObservableCollection<string> Messages { get; }
    }
}

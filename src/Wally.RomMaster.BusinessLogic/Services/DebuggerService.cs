using System;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.Domain.Interfaces;

namespace Wally.RomMaster.BusinessLogic.Services
{
    public class DebuggerService : IDebuggerService
    {
        public ILoggerProvider LoggerProvider { get; private set; }

        // public event EventHandler<string> MessageReceived;

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        public DebuggerService()
        {
            LoggerProvider = new DebuggerLoggerProvider(this);
        }

        protected void OnMessageReceived(object sender, string e)
        {
            Messages.Add(e);
            // MessageReceived?.Invoke(sender, e);
        }

        private class DebuggerLoggerProvider : ILoggerProvider
        {
            private readonly DebuggerLogger logger;

            public DebuggerLoggerProvider(DebuggerService debuggerService)
            {
                logger = new DebuggerLogger();
                logger.OnMessageReceived += debuggerService.OnMessageReceived;
            }

            public ILogger CreateLogger(string categoryName)
            {
                return logger;
            }

            public void Dispose()
            {
            }
        }

        private class DebuggerLogger : ILogger
        {
            public Action<object, string> OnMessageReceived { get; set; }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                OnMessageReceived?.Invoke(this, formatter.Invoke(state, exception));
            }
        }
    }
}

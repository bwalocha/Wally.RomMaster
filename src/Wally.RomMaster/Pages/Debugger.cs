using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.BusinessLogic.Services;

namespace Wally.RomMaster.Pages
{
    public class DebuggerModel : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Inject]
        protected ILogger<DebuggerModel> Logger { get; set; }

        [Inject]
        protected IDebuggerService DebuggerService { get; set; }

        public bool IsLoading { get; private set; }

        public int Counter { get; private set; }

        public List<string> Messages { get; } = new List<string>();

        protected override Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            DebuggerService.MessageReceived += OnMessageReceived;

            IsLoading = true;

            return Task.CompletedTask;
        }

        protected void OnMessageReceived(object sender, string e)
        {
            Counter++;
            Messages.Add(e);

            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
    }
}

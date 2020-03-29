using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.Domain.Interfaces;

namespace Wally.RomMaster.Pages
{
    public class DebuggerModel : ComponentBase
    {
        [Inject]
        protected ILogger<DebuggerModel> Logger { get; set; }

        [Inject]
        protected IDebuggerService DebuggerService { get; set; }

        public bool IsLoading { get; private set; } = true;

        // public int Counter { get; private set; }

        public ObservableCollection<string> Messages { get; private set; } // = new List<string>();

        protected override Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            // DebuggerService.MessageReceived += OnMessageReceived;
            Messages = DebuggerService.Messages;

            IsLoading = false;

            return Task.CompletedTask;
        }

        // protected void OnMessageReceived(object sender, string e)
        // {
        //     Counter++;
        //
        //     InvokeAsync(() =>
        //     {
        //         Messages.Add(e);
        //         StateHasChanged();
        //     });
        // }
    }
}

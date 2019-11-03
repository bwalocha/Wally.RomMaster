﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.Domain.Interfaces;

namespace Wally.RomMaster.Pages
{
    public class DebuggerModel : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Inject]
        protected ILogger<DebuggerModel> Logger { get; set; }

        [Inject]
        protected IDebuggerService DebuggerService { get; set; }

        public bool IsLoading { get; private set; } = true;

        public int Counter { get; private set; }

        public List<string> Messages { get; } = new List<string>();

        protected override Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            DebuggerService.MessageReceived += OnMessageReceived;

            IsLoading = false;

            return Task.CompletedTask;
        }

        protected void OnMessageReceived(object sender, string e)
        {
            Counter++;

            InvokeAsync(() =>
            {
                Messages.Add(e);
                StateHasChanged();
            });
        }
    }
}

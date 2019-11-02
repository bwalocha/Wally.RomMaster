using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RazorComponent.Grid;
using Wally.RomMaster.BusinessLogic.Services;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;
using Wally.RomMaster.Shared;

namespace Wally.RomMaster.Pages
{
    public class DatListModel : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Inject]
        protected ILogger<DebuggerModel> Logger { get; set; }

        [Inject]
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public bool IsLoading { get; private set; }

        public ObservableCollection<DatViewModel> Source { get; private set; }

        public GridOptions<DatViewModel> GridOptions { get; }

        public DatListModel()
        {
            this.GridOptions = new GridOptions<DatViewModel>
            {
                PageSize = 100,
                Columns = new GridColumn<DatViewModel>[]
                {
                    new GridColumn<DatViewModel> { Caption = "Id", Bind = (a) => new MarkupString($"<a href='/DatList/{a.Id}'>{a.Id}</a>") },
                    new GridColumn<DatViewModel> { Caption = "Name", Bind = (a) => new MarkupString(a.Name) },
                    new GridColumn<DatViewModel> { Caption = "xxx", Bind = (a) => new MarkupString(a.ToString()) }
                }
            };
        }

        protected override Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            IsLoading = true;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoDat = uow.GetReadRepository<Dat>();
                Source = new ObservableCollection<DatViewModel>(repoDat.GetAll().Select(a => new DatViewModel { Id = a.Id, Name = a.Name }));
            }

            IsLoading = false;

            var t = new System.Timers.Timer(10000);
            t.Elapsed += Tick;
            t.Start();

            return Task.CompletedTask;
        }

        private void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.InvokeAsync(() =>
            {
                this.Source.Last().Name = e.SignalTime.ToString();
                this.StateHasChanged();
            });
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RazorComponent.Grid;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;

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
                    new GridColumn<DatViewModel> { Caption = "Category", Bind = (a) => new MarkupString(a.Category) },

                    new GridColumn<DatViewModel> { Caption = "Author", Bind = (a) => new MarkupString(a.Author) },

                    new GridColumn<DatViewModel> { Caption = "Version", Bind = (a) => new MarkupString(a.Version) },

                    new GridColumn<DatViewModel> { Caption = "Date", Bind = (a) => new MarkupString(a.Date?.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)) },

                    new GridColumn<DatViewModel> { Caption = "Description", Bind = (a) => new MarkupString(a.Description) },

                    //new GridColumn<DatViewModel> { Caption = "Version", Bind = (a) => new MarkupString(a.) },

                    //new GridColumn<DatViewModel> { Caption = "Version", Bind = (a) => new MarkupString(a.Version) },
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
                Source = new ObservableCollection<DatViewModel>(repoDat.GetAll().Select(a => new DatViewModel { 
                    Id = a.Id,
                    Name = a.Name,
                    Author = a.Author,
                    Category = a.Category,
                    Date = a.Date,
                    Description = a.Description,
                    Version = a.Version,
                }));
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

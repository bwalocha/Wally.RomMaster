using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RazorComponent.Grid;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;

namespace Wally.RomMaster.Pages
{
    public class DatListDetailsModel : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Inject]
        protected ILogger<DebuggerModel> Logger { get; set; }

        [Inject]
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public bool IsLoading { get; private set; }

        public DatViewModel ViewModel { get; private set; }

        public GridOptions<GameViewModel> GridOptions { get; private set; }

        public DatListDetailsModel()
        {
            GridOptions = new GridOptions<GameViewModel>
            {
                PageSize = 100,
                Columns = new GridColumn<GameViewModel>[]
                {
                    new GridColumn<GameViewModel> { Caption = "Id", Bind = (a) => new MarkupString($"<a href='/games/{a.Id}'>{a.Id}</a>") },
                    new GridColumn<GameViewModel> { Caption = "Name", Bind = (a) => new MarkupString(a.Name) },
                    new GridColumn<GameViewModel> { Caption = "Rom Count", Bind = (a) => new MarkupString(a.RomCount.ToString(System.Globalization.CultureInfo.InvariantCulture)) },
                    new GridColumn<GameViewModel> { Caption = "Year", Bind = (a) => new MarkupString(a.Year) },
                    new GridColumn<GameViewModel> { Caption = "Description", Bind = (a) => new MarkupString(a.Description) },
                }
            };
        }

        protected override async Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            IsLoading = true;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoDat = uow.GetReadRepository<Dat>();
                var model = await repoDat
                    .FindAsync(a => a.Id == Id, c => c
                        .Include(m => m.Games)
                        .ThenInclude(m => m.Roms)
                    )
                    .ConfigureAwait(false);

                ViewModel = Mapper.Map<DatViewModel>(model);
            }

            IsLoading = false;
        }
    }
}

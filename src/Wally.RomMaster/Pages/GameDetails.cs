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
    public class GameDetailsModel : Microsoft.AspNetCore.Components.ComponentBase
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

        public GameViewModel ViewModel { get; private set; }

        public GridOptions<RomViewModel> GridOptions { get; private set; }

        public GameDetailsModel()
        {
            GridOptions = new GridOptions<RomViewModel>
            {
                PageSize = 100,
                Columns = new GridColumn<RomViewModel>[]
                {
                    new GridColumn<RomViewModel> { Caption = "Id", Bind = (a) => new MarkupString($"<a href='/roms/{a.Id}'>{a.Id}</a>") },
                    new GridColumn<RomViewModel> { Caption = "Name", Bind = (a) => new MarkupString(a.Name) },
                    new GridColumn<RomViewModel> { Caption = "Size", Bind = (a) => new MarkupString(a.Size.ToString(System.Globalization.CultureInfo.InvariantCulture)) },
                    new GridColumn<RomViewModel> { Caption = "Crc", Bind = (a) => new MarkupString(a.Crc) },
                    new GridColumn<RomViewModel> { Caption = "Md5", Bind = (a) => new MarkupString(a.Md5) },
                    new GridColumn<RomViewModel> { Caption = "Sh1", Bind = (a) => new MarkupString(a.Sha1) }
                }
            };
        }

        protected override async Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            IsLoading = true;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoDat = uow.GetReadRepository<Game>();
                var model = await repoDat
                    .FindAsync(a => a.Id == Id, c => c
                        .Include(m => m.Roms)
                    )
                    .ConfigureAwait(false);

                ViewModel = Mapper.Map<GameViewModel>(model);
            }

            IsLoading = false;
        }
    }
}

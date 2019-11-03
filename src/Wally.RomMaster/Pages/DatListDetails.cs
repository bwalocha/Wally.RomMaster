using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.Database;
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

        [Parameter]
        public int Id { get; set; }

        public bool IsLoading { get; private set; }

        public DatViewModel ViewModel { get; private set; }

        public DatListDetailsModel()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
            Logger.LogDebug("Init...");

            IsLoading = true;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoDat = uow.GetReadRepository<Dat>();
                var result = await repoDat
                    .FindAsync(a => a.Id == Id)
                    .ConfigureAwait(false);

                ViewModel = new DatViewModel {
                    Id = result.Id,
                    Name = result.Name,
                    Author = result.Author,
                    Category = result.Category,
                    Date = result.Date,
                    Description = result.Description,
                    Version = result.Version,
                    // result.File.Path
                };
            }

            IsLoading = false;
        }
    }
}

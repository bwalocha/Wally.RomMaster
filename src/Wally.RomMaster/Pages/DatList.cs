using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Wally.Database;
using Wally.RazorComponent.Grid;
using Wally.RomMaster.BusinessLogic.Services;
using Wally.RomMaster.Domain.Models;
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

        public IEnumerable<Dat> source;

        public GridOptions gridOptions;

        public DatListModel()
        {
            this.gridOptions = new GridOptions
            {
                PageSize = 100,
                Columns = new GridColumn[]
                {
                    new GridColumn { Caption = "Id", Bind = (a) => a.ToString() }, //new Func<Dat, string>(a => a.Id.ToString()) },
                    new GridColumn { Caption = "Name", Bind = (a) => a.ToString() }
                }
            };
        }

        protected override Task OnInitAsync()
        {
            Logger.LogDebug("Init...");

            IsLoading = true;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoDat = uow.GetReadRepository<Dat>();
                this.source = repoDat.GetAll();
            }

            IsLoading = false;

            return Task.CompletedTask;
        }
    }
}

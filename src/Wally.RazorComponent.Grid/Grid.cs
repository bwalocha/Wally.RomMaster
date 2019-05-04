using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wally.RazorComponent.Grid
{
    public class GridModel : ComponentBase
    {
        [Parameter]
        public GridOptions Options { get; set; } = new GridOptions();

        [Parameter]
        public IEnumerable<object> Source { get; set; }

        public int Page { get; private set; } = 0;

        public GridModel()
        {

        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Wally.RazorComponent.Grid
{
    public class GridModel<TModel> : ComponentBase// where TModel : class
    {
        [Parameter]
        public GridOptions<TModel> Options { get; set; } = new GridOptions<TModel>();

        [Parameter]
        public IEnumerable<TModel> Source { get; set; }

        public int Page { get; private set; } = 0;

        public GridModel()
        {
        }
    }
}

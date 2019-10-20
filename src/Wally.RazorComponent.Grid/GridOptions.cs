using System;

namespace Wally.RazorComponent.Grid
{
    public class GridOptions<TModel>// where TModel : class
    {
        public int PageSize { get; set; } = 20;

        public GridColumn<TModel>[] Columns { get; set; } = Array.Empty<GridColumn<TModel>>();
    }
}

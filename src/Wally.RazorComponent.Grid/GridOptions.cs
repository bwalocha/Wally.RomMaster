using System;

namespace Wally.RazorComponent.Grid
{
    public class GridOptions
    {
        public int PageSize { get; set; } = 20;

        public GridColumn[] Columns { get; set; } = Array.Empty<GridColumn>();
    }
}

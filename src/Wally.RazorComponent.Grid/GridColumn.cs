using System;

namespace Wally.RazorComponent.Grid
{
    public class GridColumn
    {
        public string Caption { get; set; }

        public Func<object, string> Bind { get; set; }
    }
}
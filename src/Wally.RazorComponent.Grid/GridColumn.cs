using System;

namespace Wally.RazorComponent.Grid
{
    public class GridColumn<TModel>// where TModel : class
    {
        public string Caption { get; set; }

        public Func<TModel, string> Bind { get; set; }
    }
}
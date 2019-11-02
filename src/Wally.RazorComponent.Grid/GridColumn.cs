using System;
using Microsoft.AspNetCore.Components;

namespace Wally.RazorComponent.Grid
{
    public class GridColumn<TModel>// where TModel : class
    {
        public string Caption { get; set; }

        public Func<TModel, MarkupString> Bind { get; set; }
    }
}
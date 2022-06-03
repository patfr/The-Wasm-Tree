using Microsoft.AspNetCore.Components;
using WasmTree.Data;
using WasmTree.Data.Interfaces;

namespace WasmTree.Pages.Components.Common
{
    public partial class Blank : ComponentBase
    {
        [Parameter] public BlankData? Data { get; set; }

        private string? _width;
        private string? _height;

        private void Render(object? sender, EventArgs e)
        {
            _width = Data?.Width?.Value ?? "8px";
            _height = Data?.Height?.Value ?? "17px";
            InvokeAsync(() => StateHasChanged());
        }

        protected override void OnInitialized()
        {
            Events.Render += Render;
        }
    }

    public class BlankData
    {
        public ICompute<string>? Width { get; set; }
        public ICompute<string>? Height { get; set; }
    }
}

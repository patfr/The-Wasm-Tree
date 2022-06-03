using Microsoft.AspNetCore.Components;
using WasmTree.Data;
using WasmTree.Data.Classes;
using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;

namespace WasmTree.Pages.Components.Layer
{
    public partial class ResetButton : ComponentBase
    {
        [Parameter] public ResetButtonData? Data { get; set; }

        private string _class = "locked";
        private string? _color;
        private string? _base;
        private string? _gain;
        private Dec? _next;
        private Dec? _actual;

        private void Render(object? sender, EventArgs e)
        {

            _base = Data?.Conversion?.Value?.Con?.Value?.Options?.Value?.BaseResource?.Value?.Name ?? "null";
            _gain = Data?.Conversion?.Value?.Con?.Value?.Options?.Value?.GainResource?.Value?.Name ?? "null";
            _next = Data?.Conversion?.Value?.Con?.Value?.NextAt() ?? 0;
            _actual = Data?.Conversion?.Value?.Con?.Value?.ActualGain() ?? 0;
            if (_actual > 0)
            {
                _color = Data?.Color?.Value ?? "#ffffff";
                _class = "can";
            }
            else
            {
                _color = "#474747";
                _class = "locked";
            }
            InvokeAsync(() => StateHasChanged());
        }

        protected override void OnInitialized()
        {
            Events.Render += Render;
        }

        private void Reset() => Data?.Conversion?.Value?.Convert();
    }

    public class ResetButtonData
    {
        public ICompute<Conversion<IConversion>>? Conversion { get; set; }
        public ICompute<string>? Color { get; set; }
    }
}

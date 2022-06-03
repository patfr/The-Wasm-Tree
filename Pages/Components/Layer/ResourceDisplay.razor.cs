using Microsoft.AspNetCore.Components;
using WasmTree.Data;
using WasmTree.Data.Classes;
using WasmTree.Data.Interfaces;
using static WasmTree.Data.Technical.Dec;

namespace WasmTree.Pages.Components.Layer
{
    public partial class ResourceDisplay : ComponentBase
    {
        [Parameter] public ResourceDisplayData? Data { get; set; }

        private Resource? _resource;
        private bool? _showGain;
        private int? _precisionOverride;
        private string? _color;
        private bool _glow;
        private Format? _formatOverride;

        private void Render(object? sender, EventArgs e)
        {
            _resource = Data?.Resource?.Value;
            _showGain = Data?.ShowGain?.Value ?? false;
            _precisionOverride = Data?.PrecisionOverride?.Value;
            _color = Data?.Color?.Value ?? "#ffffff";
            _glow = Data?.Glow?.Value ?? false;
            _formatOverride = Data?.FormatOverride?.Value;
            InvokeAsync(() => StateHasChanged());
        }

        protected override void OnInitialized()
        {
            Events.Render += Render;
        }
    }

    public class ResourceDisplayData
    {
        public ICompute<Resource>? Resource { get; set; }
        public ICompute<bool>? ShowGain { get; set; }
        public ICompute<int>? PrecisionOverride { get; set; }
        public ICompute<string>? Color { get; set; }
        public ICompute<bool>? Glow { get; set; }
        public ICompute<Format>? FormatOverride { get; set; }
    }
}

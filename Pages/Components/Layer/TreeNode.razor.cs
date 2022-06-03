using Microsoft.AspNetCore.Components;
using WasmTree.Data;
using WasmTree.Data.Interfaces;

namespace WasmTree.Pages.Components.Layer
{
    public partial class TreeNode : ComponentBase
    {
        [Parameter] public ILayerTemplate? Layer { get; set; }

        private string? _color;
        private string? _glowColor;
        private string? _symbol;

        private void Render(object? sender, EventArgs e)
        {
            _color = Layer?.TreeNode?.Value?.Color?.Value ?? "#ffffff";
            _glowColor = Layer?.TreeNode?.Value?.GlowColor?.Value ?? "#ffffff";
            _symbol = Layer?.TreeNode?.Value?.Symbol?.Value ?? "a";

            InvokeAsync(() => StateHasChanged());
        }

        protected override void OnInitialized()
        {
            Events.Render += Render;
        }
    }

    public class TreeNodeData
    {
        public ICompute<string>? Color { get; set; }
        public ICompute<string>? GlowColor { get; set; }
        public ICompute<string>? Symbol { get; set; }
    }
}

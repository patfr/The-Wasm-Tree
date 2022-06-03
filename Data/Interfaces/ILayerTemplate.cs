using WasmTree.Data.Technical;
using WasmTree.Pages.Components.Layer;

namespace WasmTree.Data.Interfaces
{
    public interface ILayerTemplate
    {
        ICompute<string>? Color { get; set; }
        ICompute<object[]>? Display { get; set; }
        ICompute<TreeNodeData>? TreeNode { get; set; }
    }
}

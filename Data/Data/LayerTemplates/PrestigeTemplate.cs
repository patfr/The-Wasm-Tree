using System.ComponentModel.DataAnnotations;
using WasmTree.Data.Common;
using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;
using WasmTree.Pages.Components.Layer;

namespace Layers
{
    public class PrestigeTemplate : ILayerTemplate
    {
        public ICompute<string>? Color { get; set; }
        public ICompute<object[]>? Display { get; set; }
        public ICompute<TreeNodeData>? TreeNode { get; set; }

        public ICompute<RowData<IRowComponent>>? Row { get; set; }
    }
}

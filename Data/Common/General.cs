using WasmTree.Data.Interfaces;

namespace WasmTree.Data.Common
{
    public class RowData<T> where T : IRowComponent
    {
        public Dictionary<string, T>? Values { get; private set; }
        public RowData(Dictionary<string, T>? values) => Values = values;
    }

    public enum Visibility
    {
        Shown = 0,
        Hidden = 1,
        None = 2,
        _ = Shown
    }
}

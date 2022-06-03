using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;

namespace WasmTree.Data.Classes
{
    public class Resource
    {
        public Resource(Dec value, string name = "points", int precision = 2, ICompute<Dec>? func = null)
        {
            Name = name;
            Value = value;
            Precision = precision;
            Gain = func is null ? new Calculated<Dec>(() => 0) : func;
        }

        public string Name { get; set; }
        public Dec Value { get; set; }
        public int Precision { get; set; }
        public ICompute<Dec> Gain { get; set; }
    }
}

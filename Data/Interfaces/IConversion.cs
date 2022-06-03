using WasmTree.Data.Classes;
using WasmTree.Data.Technical;

namespace WasmTree.Data.Interfaces
{
    public interface IConversion
    {
        ICompute<ConversionOptions>? Options { get; set; }
        Dec? NextAt();
        Dec? CurrentAt();
        Dec? CurrentGain();
        Dec? ActualGain();
        void Convert();
    }
}

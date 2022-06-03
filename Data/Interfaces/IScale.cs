using WasmTree.Data.Classes;
using WasmTree.Data.Technical;

namespace WasmTree.Data.Interfaces
{
    public interface IScale
    {
        Dec? CurrentGain(IConversion? o);
        Dec? CurrentAt(IConversion? o);
        Dec? NextAt(IConversion? o);
    }
}

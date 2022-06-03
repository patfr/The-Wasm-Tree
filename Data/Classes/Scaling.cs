using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;

namespace WasmTree.Data.Classes
{
    public class LinearScaling : IScale
    {
        public ICompute<Dec>? Base;
        public ICompute<Dec>? Coefficient;

        public Dec? CurrentGain(IConversion? o)
        {
            Dec br = o?.Options?.Value?.BaseResource?.Value?.Value ?? 0;
            Dec b = Base?.Value ?? 0;
            Dec c = Coefficient?.Value ?? 0;
            if (br < b) return 0;
            return Dec.Sub(br, b).Mul(c).Add(1).Floor();
        }

        public Dec? CurrentAt(IConversion? o)
        {
            Dec c = o?.CurrentGain() ?? 0;
            c = Dec.Max(0, c);
            return Dec.Sub(c, 1).Div(Coefficient?.Value ?? 0).Add(Base?.Value ?? 0);
        }


        public Dec? NextAt(IConversion? o)
        {
            Dec next = Dec.Add(o?.CurrentGain() ?? 0, 1);
            next = Dec.Max(0, next);
            return Dec.Sub(next, 1).Div(Coefficient?.Value ?? 0).Add(Base?.Value ?? 0).Max(Base?.Value ?? 0);
        }
    }
}

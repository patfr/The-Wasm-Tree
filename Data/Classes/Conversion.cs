using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;

namespace WasmTree.Data.Classes
{
    public class Conversion<T> where T : IConversion
    {
        public ICompute<T>? Con { get; set; }

        public Conversion(ICompute<T> con) => Con = con;

        public Dec? NextAt() => Con?.Value?.NextAt();
        public Dec? CurrentAt() => Con?.Value?.CurrentAt();
        public Dec? CurrentGain() => Con?.Value?.CurrentGain();
        public Dec? ActualGain() => Con?.Value?.ActualGain();
        public void Convert() => Con?.Value?.Convert();
    }

    public class ConversionOptions
    {
        public Func<ConversionOptions?, Dec>? NextAt;
        public Func<ConversionOptions?, Dec>? CurrentAt;
        public Func<ConversionOptions?, Dec>? CurrentGain;
        public Func<ConversionOptions?, Dec>? ActualGain;
        public ICompute<Resource>? BaseResource;
        public ICompute<Resource>? GainResource;
        public ICompute<bool>? RoundUpCost;
        public ICompute<bool>? BuyMax;
        public ICompute<IModifier[]>? GainModifier;
        public ICompute<IScale>? Scaling;
        public Action<ConversionOptions>? Convert;
    }

    public class CumulativeConversion : IConversion
    {
        public ICompute<ConversionOptions>? Options { get; set; }

        public Dec? NextAt()
        {
            if (Options?.Value?.NextAt is not null) return Options.Value.NextAt.Invoke(Options.Value);
            return StandardNextAt();
        }

        public Dec? CurrentAt()
        {
            if (Options?.Value?.CurrentAt is not null) return Options.Value.CurrentAt.Invoke(Options.Value);
            return StandardCurrentAt();
        }

        public Dec? CurrentGain()
        {
            if (Options?.Value?.CurrentGain is not null) return Options.Value.CurrentGain.Invoke(Options.Value);
            return StandardCurrentGain();
        }

        public Dec? ActualGain()
        {
            if (Options?.Value?.ActualGain is not null) return Options.Value.ActualGain.Invoke(Options.Value);
            return StandardActualGain();
        }

        public void Convert()
        {
            if (Options?.Value?.Convert is not null) { Options.Value.Convert.Invoke(Options.Value); return; }
            StandardConvert();
        }

        private Dec? StandardNextAt()
        {
            Dec c = Options?.Value?.Scaling?.Value?.NextAt(this) ?? 0;
            if (Options?.Value?.RoundUpCost?.Value ?? false) c = Dec.Ceil(c);
            return c;
        }

        private Dec? StandardCurrentAt()
        {
            Dec c = Options?.Value?.Scaling?.Value?.CurrentAt(this) ?? 0;
            if (Options?.Value?.RoundUpCost?.Value ?? false) c = Dec.Ceil(c);
            return c;
        }

        private Dec? StandardCurrentGain()
        {
            Dec gain = Options?.Value?.Scaling?.Value?.CurrentGain(this) ?? 0;
            gain = Dec.Floor(gain).Max(0);
            if (!Options?.Value?.BuyMax?.Value ?? false) gain = gain.Min(1);
            return gain;
        }

        private Dec? StandardActualGain()
        {
            return Options?.Value?.Scaling?.Value?.CurrentGain(this) ?? 0;
        }

        private void StandardConvert()
        {
            if (Options?.Value?.GainResource?.Value?.Value is not null)
                Options.Value.GainResource.Value.Value += CurrentGain() ?? 0;
            if (Options?.Value?.BaseResource?.Value?.Value is not null)
                Options.Value.BaseResource.Value.Value = 0;
        }
    }
}

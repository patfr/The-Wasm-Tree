using WasmTree.Data.Interfaces;

namespace WasmTree.Data.Technical
{
    public class Computed<T> : ICompute<T>
    {
        public T? Value { get; private set; } = default;
        private readonly Func<T>? _func;

        public Computed(Func<T> func, bool u = true)
        {
            _func = func;
            if (u)
            {
                Events.Update += Update; Update(null, new EventArgs());
                return;
            }
            Events.Render += Update; Update(null, new EventArgs());
        }

        private void Update(object? sender, EventArgs e)
        {
            if (_func == null) return;
            Value = _func.Invoke();
        }

        public static explicit operator Computed<T>(T t) => new(() => t);
    }

    public class Calculated<T> : ICompute<T>
    {
        public T? Value { get; private set; } = default;
        public Calculated(Func<T> func) => Value = func.Invoke();
        public static explicit operator Calculated<T>(T t) => new(() => t);
    }

    public class ComputedArgs<T> : ICompute<T>
    {
        public T? Value { get; private set; } = default;
        private readonly Func<EventArgs, T>? _func;

        public ComputedArgs(Func<EventArgs, T> func, bool u = true)
        {
            _func = func;
            if (u)
            {
                Events.Update += Update; Update(null, new EventArgs());
                return;
            }
            Events.Render += Update; Update(null, new EventArgs());
        }

        private void Update(object? sender, EventArgs e)
        {
            if (_func == null) return;
            Value = _func.Invoke(e);
        }

        public static explicit operator ComputedArgs<T>(T t) => new((EventArgs e) => t);
    }
}

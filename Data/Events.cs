namespace WasmTree.Data
{
    public class Events
    {
        public static event EventHandler? Update;
        public static event EventHandler? Render;

        public static void OnUpdate(UpdateEventArgs e)
        {
            Update?.Invoke(null, e);
        }

        public static void OnRender(RenderEventArgs e)
        {
            Render?.Invoke(null, e);
        }
    }

    public class UpdateEventArgs : EventArgs
    {
        public double Delta { get; set; }
    }

    public class RenderEventArgs : EventArgs
    {
        public double Delta { get; set; }
    }
}

using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;
using WasmTree.Data;
using WasmTree.Data.Classes;
using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;
using WasmTree.Pages.Components.Layer;

namespace WasmTree.Pages
{
    public partial class Index : ComponentBase
    {
        private static bool TreeOpen { get; set; } = true;
        private static bool LayerOpen { get; set; } = true;
        private static bool InfoOpen { get; set; } = false;
        private static bool SettingsOpen { get; set; } = false;
        private static bool ChangelogOpen { get; set; } = false;
        public static string ChangelogType { get; set; } = "Mod";

        public static void ToggleTree() => TreeOpen = !TreeOpen;
        public static void ToggleLayer() => LayerOpen = !LayerOpen;
        public static void ToggleInfo() => InfoOpen = !InfoOpen;
        public static void ToggleSettings() => SettingsOpen = !SettingsOpen;
        public static void ToggleChangelog() => ChangelogOpen = !ChangelogOpen;
        public static void ToggleFrameworkChangelog() { ToggleChangelog(); ChangelogType = "Base"; }
        public static void ToggleModChangelog() { ToggleChangelog(); ChangelogType = "Mod"; }
        public static void ToggleFrameworkChangelogInfo() { ToggleFrameworkChangelog(); ToggleInfo(); }
        public static void ToggleModChangelogInfo() { ToggleModChangelog(); ToggleInfo(); }

        public static bool ShowUpdateTps { get; set; } = false;
        public static bool ShowRenderTps { get; set; } = false;
        private static readonly ulong[] AverageUpdateTime = new ulong[] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
        private static readonly ulong[] AverageRenderTime = new ulong[] { 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 };
        private static int UpdateIndex = 0;
        private static int RenderIndex = 0;

        public static ulong PlayTime { get; set; } = 0;

        public static readonly Computed<Dictionary<string, ICompute<ILayerTemplate>>> Layers = new(CreateLayers, false);
        private static Dictionary<string, ICompute<ILayerTemplate>> CreateLayers()
        {
            Dictionary<string, ICompute<ILayerTemplate>> layers = new();

            for (int i = 0; i < Mod.LayerNames.Length; i++)
            {
                try
                {
                    Type? t = Type.GetType($"Layers.{Mod.LayerNames[i]}");
                    if (t is null) throw new Exception("Could not find layer");
                    if (t.GetField("Layer")?.GetValue(t) is not ICompute<ILayerTemplate> l) throw new Exception("Could not find field Layer");
                    layers.Add(Mod.LayerNames[i], l);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return layers;
        }

        public static double CalcTps(ulong[] t) => Math.Round(10000 / (double)(t[0] + t[1] + t[2] + t[3] + t[4] + t[5] + t[6] + t[7] + t[8] + t[9]));

        public static string FormatTime(ulong t)
        {
            double n = (double)t / 1000;
            int s = (int)(n % 60);
            n /= 60;
            int m = (int)(n % 60);
            n /= 60;
            int h = (int)(n % 24);
            n /= 24;
            int d = (int)n;

            if (d > 0)
                return $"{d}d {h}h {m}m {s}s";
            if (h > 0)
                return $"{h}h {m}m {s}s";
            if (m > 0)
                return $"{m}m {s}s";
            return $"{s}s";
        }

        private static void Update(double d)
        {
            ulong t = (ulong)d;
            PlayTime += t;
            AverageUpdateTime[UpdateIndex] = t;
            UpdateIndex = (UpdateIndex + 1) % 10;
            double i = d / 1000d;
            if (Mod.Points.Resource?.Value?.Gain.Value is not null)
                Mod.Points.Resource.Value.Value += Mod.Points.Resource.Value.Gain.Value * i;
            Events.OnUpdate(new() { Delta = i });
        }

        private static void Render(double d)
        {
            AverageRenderTime[RenderIndex] = (ulong)d;
            RenderIndex = (RenderIndex + 1) % 10;
            Events.OnRender(new() { Delta = d / 1000 });
        }

        protected override void OnInitialized()
        {
            Times++;
            if (Times != 2) return;
            Thread.CurrentThread.CurrentCulture = new("en-US");
            Initialized = true;
            LastUpdate = GetTimeMs() - 20;
            LastRender = LastUpdate;

            UpdateTimer = new Timer((e) =>
            {
                long time = GetTimeMs();
                Update(time - LastUpdate);
                LastUpdate = time;
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));

            RenderTimer = new Timer((e) =>
            {
                long time = GetTimeMs();
                Render(time - LastRender);
                InvokeAsync(() => StateHasChanged());
                LastRender = time;
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(50));
        }

        private static long GetTimeMs() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        private static long LastUpdate { get; set; }
        private static long LastRender { get; set; }
        private static bool Initialized = false;
        private static int Times = 0;

        private static Timer? UpdateTimer { get; set; }
        private static Timer? RenderTimer { get; set; }
    }
}
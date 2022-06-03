using Layers;
using WasmTree.Data.Classes;
using WasmTree.Data.Technical;
using WasmTree.Data.Versions;
using WasmTree.Pages.Components.Layer;

namespace WasmTree.Data
{
    public class Mod
    {
        public static string ExternalName { get; } = "Demo";
        public static string InternalName { get; } = "[Name here]";
        public static string Description { get; } = "This is a demo";
        public static string Author { get; } = "[Name here]";
        public static string PointsName { get; } = "points";

        public static string CurrentLayer { get; set; } = "Prestige";
        public static string[] LayerNames { get; } = new string[] { "Prestige" };

        public static Dictionary<string, VersionData> ChangeLog { get; } = new()
        {
            {
                "0.1",
                new()
                {
                    ExternalName = "Default",
                    ExternalVersion = "0.1",
                    VersionDate = new DateTime(2022, 6, 3),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Added stuff" },
                        new() { Type = "Changed", Description = "Changed stuff" },
                        new() { Type = "Removed", Description = "Removed stuff" },
                        new() { Type = "Fixed", Description = "Fixed stuff" },
                        new() { Type = "Breaking", Description = "Fixed stuff" },
                        new() { Type = "Balancing", Description = "Balanced stuff" },
                    }
                }
            }
        };

        public static VersionData CurrentVersion { get; } = ChangeLog.First().Value;

        public static readonly Computed<Dec> Gain = new(PointGain);
        private static Dec PointGain()
        {
            Dec gain = 0;
            if (Pages.Index.Layers?.Value?["Prestige"].Value is PrestigeTemplate l)
            {
                if (l.Row?.Value?.Values?["Start"] is UpgradeData u1)
                    if (u1.Bought) gain++;
                if (l.Row?.Value?.Values?["Add"] is UpgradeData u2)
                    if (u2.Bought) gain += 5;
                if (l.Row?.Value?.Values?["Mul"] is UpgradeData u3)
                    if (u3.Bought) gain *= 5;
            }
            return gain;
        }

        public static readonly Resource Res = new(10, func: Gain);
        public static ResourceDisplayData Points { get; set; } = new()
        {
            Resource = (Calculated<Resource>)Res,
            ShowGain = (Calculated<bool>)true
        };
    }
}

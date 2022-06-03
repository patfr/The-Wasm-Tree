using WasmTree.Data.Versions;

namespace WasmTree.Data.Technical
{
    public class Framework
    {
        public static string ExternalName { get; } = "The Wasm Tree";

        public static Dictionary<string, VersionData> ChangeLog { get; } = new()
        {
            {
                "0.1.0",
                new()
                {
                    ExternalName = "Release",
                    ExternalVersion = "0.1.0",
                    VersionDate = new DateTime(2022, 6, 3),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Reset functionality" },
                        new() { Type = "Added", Description = "Basic Demo" },
                    }
                }
            },
            {
                "0.0.8",
                new()
                {
                    ExternalName = "Pre-release 8",
                    ExternalVersion = "0.0.8",
                    VersionDate = new DateTime(2022, 5, 30),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Upgrade" },
                    }
                }
            },
            {
                "0.0.7",
                new()
                {
                    ExternalName = "Pre-release 7",
                    ExternalVersion = "0.0.7",
                    VersionDate = new DateTime(2022, 5, 29),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "ResetButton" },
                        new() { Type = "Added", Description = "Blank" },
                        new() { Type = "Added", Description = "Visibility enum" },
                    }
                }
            },
            {
                "0.0.6",
                new()
                {
                    ExternalName = "Pre-release 6",
                    ExternalVersion = "0.0.6",
                    VersionDate = new DateTime(2022, 5, 25),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Ported break_eternity.ts(.js) to c#." },
                        new() { Type = "Changed", Description = "ResourceDisplay so it uses the new number library." },
                        new() { Type = "Changed", Description = "Resource so it uses the new number library." },
                        new() { Type = "Changed", Description = "Points uses the new number library." },
                    }
                }
            },
            {
                "0.0.5",
                new()
                {
                    ExternalName = "Pre-release 5",
                    ExternalVersion = "0.0.5",
                    VersionDate = new DateTime(2022, 5, 23),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Framework version on navbar." },
                        new() { Type = "Added", Description = "Layer structure." },
                        new() { Type = "Changed", Description = "TreeNode code to fit the new layer structure." },
                        new() { Type = "Changed", Description = "ResourceDisplay code to fit the new layer structure." },
                        new() { Type = "Changed", Description = "ResourceDisplay so it can show in a layer." },
                        new() { Type = "Changed", Description = "Some css to make it more clean." },
                    }
                }
            },
            {
                "0.0.4",
                new()
                {
                    ExternalName = "Pre-release 4",
                    ExternalVersion = "0.0.4",
                    VersionDate = new DateTime(2022, 5, 22),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "TreeNode." },
                        new() { Type = "Changed", Description = "It so there will NEVER be themes. \"Rage Quit\"" },
                    }
                }
            },
            {
                "0.0.3",
                new()
                {
                    ExternalName = "Pre-release 3",
                    ExternalVersion = "0.0.3",
                    VersionDate = new DateTime(2022, 5, 21),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Changelog." },
                        new() { Type = "Added", Description = "Settings." },
                        new() { Type = "Added", Description = "Toggles for TPS display." },
                    }
                }
            },
            {
                "0.0.2",
                new()
                {
                    ExternalName = "Pre-release 2",
                    ExternalVersion = "0.0.2",
                    VersionDate = new DateTime(2022, 5, 20),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Info menu." },
                        new() { Type = "Added", Description = "TPS display." },
                    }
                }
            },
            {
                "0.0.1",
                new()
                {
                    ExternalName = "Pre-release 1",
                    ExternalVersion = "0.0.1",
                    VersionDate = new DateTime(2022, 5, 19),
                    Changes = new Change[]
                    {
                        new() { Type = "Added", Description = "Layer & Tree views." },
                    }
                }
            }
        };

        public static VersionData CurrentVersion { get; } = ChangeLog.First().Value;
    }
}

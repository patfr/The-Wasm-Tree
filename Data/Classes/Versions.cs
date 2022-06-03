namespace WasmTree.Data.Versions
{
    public class VersionData
    {
        public string? ExternalName { get; set; }
        public string? ExternalVersion { get; set; }
        public DateTime? VersionDate { get; set; }
        public Change[]? Changes { get; set; }
    }

    public class Change
    {
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}

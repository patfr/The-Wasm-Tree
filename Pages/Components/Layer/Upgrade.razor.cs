using Microsoft.AspNetCore.Components;
using WasmTree.Data;
using WasmTree.Data.Classes;
using WasmTree.Data.Interfaces;
using WasmTree.Data.Technical;

namespace WasmTree.Pages.Components.Layer
{
    public partial class Upgrade : ComponentBase
    {
        [Parameter] public UpgradeData? Data { get; set; }
        [Parameter] public string Style { get; set; } = "";

        private string _class = "locked";
        private string? _title;
        private string? _desc;
        private string? _color;
        private Resource? _resource;
        private Dec? _cost;

        private void Render(object? sender, EventArgs e)
        {
            _title = Data?.Title?.Value ?? "";
            _desc = Data?.Description?.Value ?? "";
            _cost = Data?.Cost?.Value ?? 0;
            _resource = Data?.Resource?.Value ?? new(0);
            if (Data?.Bought ?? false)
            {
                _color = Data?.BoughtColor?.Value ?? (Data?.Color?.Value ?? "#ffffff");
                _class = "bought";
            }
            else
            {
                if (StandardCanAfford(Data))
                {
                    _color = Data?.Color?.Value ?? "#ffffff";
                    _class = "can";
                }
                else
                {
                    _color = Data?.LockedColor?.Value ?? "#474747";
                    _class = "locked";
                }
            }
            InvokeAsync(() => StateHasChanged());
        }

        protected override void OnInitialized()
        {
            Events.Render += Render;
        }

        public bool StandardCanAfford(UpgradeData? u) => u?.CanAfford?.Invoke(u) ?? StandardCanBuy(u);
        public bool StandardCanBuy(UpgradeData? u) => u?.CanBuy?.Invoke(u) ?? (u?.Resource?.Value?.Value ?? 0) >= (u?.Cost?.Value ?? -1);
        public void StandardBuy()
        {
            if (Data?.Buy is null) { Buy(Data); return; }
            Data.Buy.Invoke(Data);
        }

        private void Buy(UpgradeData? u)
        {
            if (u?.Bought ?? false) return;
            if (!StandardCanAfford(u)) return;
            if (u is null) return;
            if (u?.Resource?.Value?.Value is null) return;
            u.Bought = true;
            u!.Resource.Value.Value -= u?.Cost?.Value ?? 0;
        }
    }

    public class UpgradeData : IRowComponent
    {
        public ICompute<string>? Title { get; set; }
        public ICompute<string>? Description { get; set; }
        public ICompute<string>? Color { get; set; }
        public ICompute<string>? LockedColor { get; set; }
        public ICompute<string>? BoughtColor { get; set; }
        public ICompute<Resource>? Resource { get; set; }
        public ICompute<Dec>? Cost { get; set; }
        public bool Bought { get; set; } = false;

        public Func<UpgradeData, bool>? CanAfford { get; set; }
        public Func<UpgradeData, bool>? CanBuy { get; set; }
        public Action<UpgradeData?>? Buy { get; set; }
    }
}

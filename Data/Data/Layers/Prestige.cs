using WasmTree.Data.Technical;
using WasmTree.Data.Interfaces;
using WasmTree.Pages.Components.Layer;
using WasmTree.Data.Classes;
using WasmTree.Pages.Components.Common;
using WasmTree.Data;
using WasmTree.Data.Common;

namespace Layers
{
    public class Prestige
    {
        private static readonly Calculated<TreeNodeData> TreeNode = new(CreateTreeNode);
        private static readonly Calculated<Resource> Points = new(CreateResource);
        private static readonly Calculated<ResourceDisplayData> ResourceDisplay = new(CreateResourceDisplay);
        private static readonly Calculated<Conversion<IConversion>> ResetConversion = new(CreateConversion);
        private static readonly Calculated<ResetButtonData> ResetButton = new(CreateResetButton);
        private static readonly Calculated<RowData<IRowComponent>> UpgradeRow = new(CreateUpgradeRow);
        private static readonly Calculated<object[]> Display = new(CreateDisplay);

        private static object[] CreateDisplay() => new object[]
        {
            ResourceDisplay,
            (Calculated<BlankData>)new BlankData(),
            ResetButton,
            (Calculated<BlankData>)new BlankData(),
            UpgradeRow,
        };

        private static RowData<IRowComponent> CreateUpgradeRow() => new(new Dictionary<string, IRowComponent>()
        {
            {
                "Start",
                new UpgradeData()
                {
                    Title = (Calculated<string>)"Start",
                    Description = (Calculated<string>)"Gain 1 points per second",
                    Color = (Calculated<string>)"#3b97ed",
                    BoughtColor = (Calculated<string>)"#79b5ed",
                    Resource = Points,
                    Cost = (Calculated<Dec>)(Dec)1,
                }
            },
            {
                "Add",
                new UpgradeData()
                {
                    Title = (Calculated<string>)"Addition",
                    Description = (Calculated<string>)"Add 5 to point gain",
                    Color = (Calculated<string>)"#3b97ed",
                    BoughtColor = (Calculated<string>)"#79b5ed",
                    Resource = Points,
                    Cost = (Calculated<Dec>)(Dec)2,
                }
            },
            {
                "Mul",
                new UpgradeData()
                {
                    Title = (Calculated<string>)"Multiply",
                    Description = (Calculated<string>)"Multiply point gain by 5",
                    Color = (Calculated<string>)"#3b97ed",
                    BoughtColor = (Calculated<string>)"#79b5ed",
                    Resource = Points,
                    Cost = (Calculated<Dec>)(Dec)50,
                }
            }
        });

        private static Conversion<IConversion> CreateConversion() => new((Calculated<IConversion>)new CumulativeConversion()
        {
            Options = (Calculated<ConversionOptions>)new ConversionOptions()
            {
                Scaling = (Calculated<IScale>)new LinearScaling()
                {
                    Base = (Calculated<Dec>)(Dec)10,
                    Coefficient = (Calculated<Dec>)(Dec)0.5,
                },
                BaseResource = Mod.Points.Resource,
                GainResource = Points,
                BuyMax = (Calculated<bool>)true,
            }
        });

        private static ResetButtonData CreateResetButton() => new()
        {
            Conversion = ResetConversion,
            Color = (Calculated<string>)"#79b5ed",
        };

        private static Resource CreateResource() => new(0, "Prestige points", 0);

        private static ResourceDisplayData CreateResourceDisplay() => new()
        {
            Resource = Points,
            Color = (Calculated<string>)"#79b5ed",
            Glow = (Calculated<bool>)true,
            PrecisionOverride = (Calculated<int>)0,
        };

        private static TreeNodeData CreateTreeNode() => new()
        {
            Color = (Calculated<string>)"#79b5ed",
            Symbol = (Calculated<string>)"p",
        };

        private static PrestigeTemplate CreateLayer() => new()
        {
            Display = Display,
            TreeNode = TreeNode,
            Color = (Calculated<string>)"#79b5ed",
            Row = UpgradeRow,
        };

        public static readonly Calculated<ILayerTemplate> Layer = new(CreateLayer);
    }
}

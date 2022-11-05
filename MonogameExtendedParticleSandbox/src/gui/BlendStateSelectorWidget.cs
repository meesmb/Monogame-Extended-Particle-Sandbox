using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class BlendStateSelectorWidget
    {
        private Dictionary<string, BlendState> blendStates = new Dictionary<string, BlendState>()
        {
            { "Alpha Blend", BlendState.AlphaBlend },
            { "Additive", BlendState.Additive },
            { "Non Pre Multiplied", BlendState.NonPremultiplied },
            { "Opaque", BlendState.Opaque }
        };

        public BlendStateSelectorWidget(Grid parent, GridSizeHolder holder)
        {
            var grid = new Grid()
            {
                GridRow = holder.RowCount++,
                GridColumn = 0,
            };
            grid.ColumnsProportions.Add(new Proportion());
            grid.RowsProportions.Add(new Proportion());
            parent.AddChild(grid);

            var label = new Label()
            {
                GridRow = 0,
                GridColumn = 0,
                Text = "Blend State: "
            };
            grid.AddChild(label);
            var selector = GUI.createComboBox(grid, 0, 1, GUI.convertDictionaryToList(blendStates),
                (i, box) =>
                {
                    Game1.blendState = blendStates[box.Items[i].Text];
                });

            selector.SelectedIndex = 0;
        }
    }
}

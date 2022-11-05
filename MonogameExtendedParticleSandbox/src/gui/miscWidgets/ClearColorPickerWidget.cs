using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.miscWidgets
{
    public class ClearColorPickerWidget
    {
        public ClearColorPickerWidget(Grid parent, GridSizeHolder holder)
        {
            Grid grid = new Grid()
            {
                GridRow = holder.RowCount++,
                GridColumn = 0,
            };
            parent.AddChild(grid);
            grid.RowsProportions.Add(new Proportion());
            grid.ColumnsProportions.Add(new Proportion());

            var picker = GUI.createColorPicker(grid, "Clear Color", 0, 0, Game1.clearColor, (color =>
            {
                Game1.clearColor = color.ToRgb();
            }));
        }
    }
}

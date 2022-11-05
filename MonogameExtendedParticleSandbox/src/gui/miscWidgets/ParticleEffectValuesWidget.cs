using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.miscWidgets
{
    public class ParticleEffectValuesWidget
    {
        private Grid parent;
        private Grid grid;
        private GridSizeHolder gridSize;
        public ParticleEffectValuesWidget(Grid parent, GridSizeHolder holder, ParticleController controller)
        {
            grid = new Grid()
            {
                GridRow = holder.RowCount++,
                GridColumn = 0,
            };
            parent.AddChild(grid);
            gridSize = new GridSizeHolder();

            grid.ColumnsProportions.Add(new Proportion());
            grid.RowsProportions.Add(new Proportion());
            grid.RowsProportions.Add(new Proportion());

            GUI.createDualSpinButton(grid, 0, 0, "Position: ", i =>
            {
                var pos = controller.getEffect().Position;
                pos.X = i;
                controller.getEffect().Position = pos;
            }, i =>
            {
                var pos = controller.getEffect().Position;
                pos.Y = i;
                controller.getEffect().Position = pos;
            });
        }
    }
}

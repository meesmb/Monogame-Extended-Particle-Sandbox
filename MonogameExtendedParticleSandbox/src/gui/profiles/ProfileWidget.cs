using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
{
    public class ProfileWidget : DeletableListWidget
    {
        protected Grid parent;
        protected int row;
        protected ParticleEmitter particleEmitter;
        protected Grid grid;

        public ProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            this.parent = parent;
            this.row = row;
            this.particleEmitter = particleEmitter;
        }

        public virtual ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new ProfileWidget(parent, row, particleEmitter);
        }

        protected Grid buildGrid(Grid parent, int row, int rowsInGrid, int columnsInGrid)
        {
            var grid = new Grid()
            {
                GridRow = row,
                GridColumn = 1,
            };
            for (int i = 0; i < columnsInGrid; i++)
                grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rowsInGrid; i++)
                grid.RowsProportions.Add(new Proportion());
            parent.AddChild(grid);
            this.grid = grid;
            return grid;
        }

        protected override void onDelete()
        {
            parent.RemoveChild(grid);
        }
        public override string export()
        {
            throw new NotImplementedException();
        }
    }
}

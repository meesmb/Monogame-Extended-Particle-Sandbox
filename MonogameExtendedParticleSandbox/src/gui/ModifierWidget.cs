using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ModifierWidget
    {
        protected Grid parent;
        protected int row;
        protected ParticleEmitter emitter;
        protected Grid grid;
        protected Label label;
        protected Modifier modifier;
        public TextButton deleteButton { get; set; }

        public ModifierWidget(Grid parent, int row, ParticleEmitter emitter)
        {
            this.parent = parent;
            this.emitter = emitter;
            this.row = row;
        }
        
        public void setRow(int row)
        {
            grid.GridRow = row + 1;
            label.GridRow = row;
        }

        public int getRow()
        {
            return grid.GridRow;
        }
        public virtual ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new ModifierWidget(parent, row, emitter);
        }

        public virtual void delete()
        {
            parent.RemoveChild(grid);
            parent.RemoveChild(label);
            emitter.Modifiers.Remove(modifier);
        }

        protected Label buildLabel(Grid parent, string name, int row)
        {
            var label = new Label()
            {
                Text = name,
                GridColumn = 1,
                GridRow = row - 1
            };
            parent.AddChild(label);
            this.label = label;
            return label;
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
    }
}

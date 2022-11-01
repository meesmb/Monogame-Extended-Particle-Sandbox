using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public abstract class DeletableListWidget
    {
        protected Grid parent;
        protected Grid grid;
        protected Label label;
        protected int row;
        public TextButton deleteButton { get; set; }

        public void setRow(int row)
        {
            if (grid != null)
                grid.GridRow = row + 1;
            label.GridRow = row;
        }

        public int getRow()
        {
            if (grid == null) return 0;
            return grid.GridRow;
        }

        public void delete()
        {
            if (parent != null)
            {
                    parent.RemoveChild(grid);
                    parent.RemoveChild(label);
            }

            onDelete();
        }

        protected abstract void onDelete();

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
    }
}

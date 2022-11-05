using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogameExtendedParticleSandbox.src.gui.profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.miscWidgets
{
    public class WidgetSelectWidget
    {
        private DeletableListWidget currentWidget;
        private Grid topGrid, widgetGrid, grid;
        private Label label;
        private ComboBox combo;
        public WidgetSelectWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder,
            List<ListItem> widgetTypes, string name, Func<Grid, int, string, DeletableListWidget> onCreate)
        {
            topGrid = topLevelGrid;
            widgetGrid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0,
            };
            topLevelGrid.AddChild(widgetGrid);

            grid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0
            };

            label = new Label()
            {
                GridColumn = 0,
                GridRow = 0,
                Text = name
            };
            widgetGrid.AddChild(label);

            combo = GUI.createComboBox(widgetGrid, 0, 1, widgetTypes, (v, combo) =>
            {
                var key = combo.Items[v].Text;
                if (currentWidget != null)
                    currentWidget.delete();
                //currentWidget = profiles[key].create(grid, 0, emitter);
                currentWidget = onCreate(grid, 0, key);
            }, 0);
            //currentWidget = profiles[combo.Items[0].Text].create(grid, 0, emitter);
            currentWidget = onCreate(grid, 0, combo.Items[0].Text);

            var rows = 10;
            grid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = 50
            });
            grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                grid.RowsProportions.Add(new Proportion());

            topLevelGrid.AddChild(grid);
        }

        public void delete()
        {
            widgetGrid.RemoveChild(label);
            widgetGrid.RemoveChild(combo);
            topGrid.RemoveChild(grid);
            topGrid.RemoveChild(widgetGrid);
        }
    }
}

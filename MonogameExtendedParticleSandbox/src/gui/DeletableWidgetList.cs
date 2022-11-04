using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class DeletableWidgetList
    {
        private string currentWidgetType = "";

        private List<DeletableListWidget> modifierWidgets = new List<DeletableListWidget>();

        public TextButton textButton { get; set; }
        public ComboBox combo { get; set; }

        private Grid topLevelGrid, selectionGrid, modifierGrid;

        private readonly int MAX_ROWS = 20;

        public DeletableWidgetList(Grid topLevelGrid, string addButtonText, GridSizeHolder topLevelGridSizeHolder, List<ListItem> widgets, Func<Grid, int, string, DeletableListWidget> onCreate)
        {
            this.topLevelGrid = topLevelGrid;
            selectionGrid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0,
            };
            topLevelGrid.AddChild(selectionGrid);

            textButton = new TextButton()
            {
                Text = addButtonText,
                GridRow = 0,
                GridColumn = 0
            };
            selectionGrid.AddChild(textButton);

            combo = GUI.createComboBox(selectionGrid, 0, 1, widgets, (v, combo) =>
            {
                currentWidgetType = combo.Items[v].Text;
            }, 0);
            currentWidgetType = combo.Items[0].Text;

            modifierGrid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                ShowGridLines = true
            };

            modifierGrid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = 50,
            });
            for (int i = 0; i < MAX_ROWS; i++)
                modifierGrid.RowsProportions.Add(new Proportion());
            topLevelGrid.AddChild(modifierGrid);

            var modifierGridRowNum = new GridSizeHolder();

            textButton.Click += (s, e) =>
            {
                var deleteButton = new TextButton()
                {
                    Text = "-",
                    GridColumn = 0,
                    GridRow = modifierGridRowNum.RowCount++,
                };
                modifierGrid.AddChild(deleteButton);

                var modifier = onCreate(modifierGrid, modifierGridRowNum.RowCount++, currentWidgetType);

                modifier.deleteButton = deleteButton;

                deleteButton.Click += (s, e) =>
                {
                    modifierGrid.RemoveChild(deleteButton);

                    modifier.delete();

                    int pos = modifierWidgets.IndexOf(modifier);

                    if (pos != -1)
                    {
                        for (int i = pos + 1; i < modifierWidgets.Count; i++)
                        {
                            int row = modifierWidgets[i].getRow();
                            var newRow = (row - 3 < 0) ? 0 : row - 3;
                            modifierWidgets[i].setRow(newRow);

                            int deleteButtonRow = modifierWidgets[i].deleteButton.GridRow;
                            int newDeleteButtonRow = (deleteButtonRow - 2 < 0) ? 0 : deleteButtonRow - 2;
                            modifierWidgets[i].deleteButton.GridRow = newDeleteButtonRow;
                        }

                        modifierWidgets.RemoveAt(pos);
                        modifierGridRowNum.RowCount -= 2;

                    }

                };

                modifierWidgets.Add(modifier);
            };
        }

        public void delete()
        {
            foreach (var mod in modifierWidgets)
            {
                mod.delete();
            }

            selectionGrid.RemoveChild(textButton);
            selectionGrid.RemoveChild(combo);

            topLevelGrid.RemoveChild(modifierGrid);
            topLevelGrid.RemoveChild(selectionGrid);
            
        }
    }
}

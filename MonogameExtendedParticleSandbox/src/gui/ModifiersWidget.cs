﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ModifiersWidget
    {
        private Dictionary<string, ModifierWidget> widgetTypes = new Dictionary<string, ModifierWidget>()
        {
            {"Age", new AgeModifierWidget()}, // needs interpolators
            {"Circle", new CircleContainerModifierWidget()},
            {"RectangleLoop", new RectangleLoopContainerModifierWidget()},
            {"Drag", new DragModifierWidget()},
            {"Linear Gravity", new LinearGravityModifierWidget()},
            {"Opacity Fast Fade", new OpacityFastFadeModifierWidget()},
            {"Rotation Rate", new RotationModifierWidget()},
            {"Velocity Color", new VelocityColorModifierWidget()}, // needs color selector (HSL)
            {"Velocity", new VelocityModifierWidget()}, // needs color selector (HSL) and interpolators
            {"Vortex", new VortexModifierWidget()},

        };
        private string currentWidgetType = "Age";

        private List<ModifierWidget> modifierWidgets = new List<ModifierWidget>();

        public TextButton textButton { get; set; }
        public ComboBox combo { get; set; }

        public ModifiersWidget(Grid topLevelGrid, int rows, ParticleController controller, int index)
        {
            // modifiers
            textButton = new TextButton()
            {
                Text = "New Modifier",
                GridRow = 6,
                GridColumn = 0
            };
            combo = new ComboBox()
            {
                GridRow = 6,
                GridColumn = 1,
            };
            var items = createModifierWidgets();
            foreach (var item in items)
                combo.Items.Add(item);

            var modifierGrid = new Grid()
            {
                GridRow = 1,
                RowSpacing = 25,
                ShowGridLines = true,
            };
            modifierGrid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = 50
            });
            for (int i = 0; i < rows; i++)
                modifierGrid.RowsProportions.Add(new Proportion());
            topLevelGrid.AddChild(modifierGrid);

            var modifierGridRowNum = new gridSizeHolder();
            combo.SelectedIndexChanged += (s, e) =>
            {
                if (combo.SelectedIndex != null)
                    currentWidgetType = combo.Items[(int)combo.SelectedIndex].Text;
            };
            textButton.Click += (s, e) =>
            {
                var deleteButton = new TextButton()
                {
                    Text = "-",
                    GridColumn = 0,
                    GridRow = modifierGridRowNum.RowCount,
                };
                modifierGrid.AddChild(deleteButton);
                ++modifierGridRowNum.RowCount;
                var modifier = widgetTypes[currentWidgetType].create(modifierGrid, modifierGridRowNum.RowCount++, controller.getEmitter(index));
                modifier.deleteButton = deleteButton;

                deleteButton.Click += (s, e) =>
                {
                    modifierGrid.RemoveChild(deleteButton);

                    modifier.delete();

                    int pos = modifierWidgets.IndexOf(modifier);
                    Console.Out.WriteLine(modifier + " at: " + pos + ", len: " + modifierWidgets.Count);
                    if (pos != -1)
                    {
                        for (int i = pos + 1; i < modifierWidgets.Count; i++)
                        {
                            int row = modifierWidgets[i].getRow();
                            var newRow = (row - 3 < 0) ? 0 : row - 3;
                            modifierWidgets[i].setRow(newRow);

                            int deleteButtonRow = modifierWidgets[i].deleteButton.GridRow;
                            int newDeleteButtonRow = (deleteButtonRow - 2 < 0) ? 0 : deleteButtonRow - 2;
                            Console.Out.WriteLine(newDeleteButtonRow + ", " + i);
                            modifierWidgets[i].deleteButton.GridRow = newDeleteButtonRow;
                        }

                        modifierWidgets.RemoveAt(pos);
                        modifierGridRowNum.RowCount -= 2;

                    }

                };


                modifierWidgets.Add(modifier);
            };
        }
        private List<ListItem> createModifierWidgets()
        {
            List<ListItem> items = new List<ListItem>();

            foreach (var widget in this.widgetTypes)
            {
                var item = new ListItem
                {
                    Text = widget.Key
                };
                items.Add(item);
            }

            return items;
        }
    }
}

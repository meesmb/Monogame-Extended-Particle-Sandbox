using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.ColorPicker;
using Myra.Graphics2D.UI.File;
using Myra.Utility;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class GUI
    {
        private static Desktop desktop;
        private Dictionary<string, SpinButton> spinButtons = new Dictionary<string, SpinButton>();

        private GridSizeHolder gridSizeHolder = new GridSizeHolder();
        private Grid topGrid;

        private DeletableWidgetList widgets;
        private BlendStateSelectorWidget blendStateSelector;

        private Dictionary<string, ParticleEmitterWidget> particleEmitters =
            new Dictionary<string, ParticleEmitterWidget>()
            {
                { "Default", new ParticleEmitterWidget() }
            };

        public GUI(ParticleController controller)
        {
            topGrid = new Grid();
            topGrid.ColumnsProportions.Add(new Proportion());
            topGrid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < 3; i++)
                topGrid.RowsProportions.Add(new Proportion());
            gridSizeHolder.ColumnCount++;

            blendStateSelector = new BlendStateSelectorWidget(topGrid, gridSizeHolder);

            widgets = new DeletableWidgetList(topGrid, "Create Emitter", gridSizeHolder,
                convertDictionaryToList(particleEmitters),
                (grid, i, arg3) =>
                {
                    topGrid.RowsProportions.Add(new Proportion());
                    var holder = new GridSizeHolder();
                    holder.RowCount = i;
                    holder.ColumnCount = gridSizeHolder.ColumnCount;
                    return new ParticleEmitterWidget(controller, grid, holder);
                });

            var pane = new HorizontalSplitPane();
            var scrollViewer = new ScrollViewer();
            pane.Widgets.Add(scrollViewer);
            scrollViewer.Content = topGrid;


            // Add it to the desktop
            desktop = new Desktop();
            desktop.Root = pane;
        }

        public static FileDialog createFileDialog(Grid parent, int row, string name, Action<string> onClose)
        {
            var button = new TextButton()
            {
                Text = name,
                GridRow = row,
                GridColumn = 0,
            };

            var dialog = new FileDialog(FileDialogMode.OpenFile)
            {
            };
            button.Click += (s, e) =>
            {
                dialog.ShowModal(GUI.desktop);
            };

            dialog.Closed += (s, e) =>
            {
                if (dialog.Result)
                {
                    onClose(dialog.FilePath);
                }
            };

            parent.AddChild(button);
            return dialog;
        }

        public static SpinButton createSpinButton(Grid parent, string name, int row, int buttonColumn = 1, bool showLable = true, int decimalPlaces = 0)
        {
            if (showLable)
            {
                var label = new Label()
                {
                    Text = name,
                    GridRow = row,
                    GridColumn = buttonColumn - 1,
                };
                parent.Widgets.Add(label);
            }

            // Spin button
            var button = new SpinButton
            {
                GridColumn = buttonColumn,
                GridRow = row,
                Width = 100,
                Nullable = false,
                DecimalPlaces = decimalPlaces
            };
            parent.Widgets.Add(button);
            return button;
        }

        public static ColorPickerDialog createColorPicker(Grid parent, string name, int row, int column, Color defaultColor, Action<HslColor> callback)
        {
            var button = new ImageTextButton()
            {
                GridRow = row,
                GridColumn = column,
                TextColor = defaultColor,
                Text = name,
            };

            var colorPicker = new ColorPickerDialog()
            {
                Title = name,
                GridRow = row,
                GridColumn = column + 1,
            };

            parent.AddChild(button);

            button.PressedChanged += (s, e) =>
            {
                if (button.IsPressed)
                {
                    var shouldOpen = !parent.GetChildren().Contains(colorPicker);
                    if (shouldOpen)
                        parent.AddChild(colorPicker);
                    else
                        colorPicker.Close();
                }
            };

            colorPicker.ArrangeUpdated += (s, e) =>
            {
                var hsl = HslColor.FromRgb(colorPicker.Color);
                button.TextColor = colorPicker.Color;
                callback(hsl);
            };
            colorPicker.Color = defaultColor;

            return colorPicker;
        }

        public static void createDualSpinButton(Grid parent, int row, int column, string name,
            Action<int> callback1, Action<int> callback2, int default1 = 0, int default2 = 0)
        {
            var label = new Label()
            {
                Text = name,
                GridRow = row,
                GridColumn = column
            };
            var button1 = new SpinButton()
            {
                GridColumn = column + 1,
                GridRow = row,
                Nullable = false,
                Value = default1,
            };
            var button2 = new SpinButton()
            {
                GridColumn = column + 2,
                GridRow = row,
                Nullable = false,
                Value = default2,
            };

            parent.AddChild(label);
            parent.AddChild(button1);
            parent.AddChild(button2);

            button1.ValueChanged += (s, e) =>
            {
                if (button1.Value != null)
                    callback1((int)button1.Value);
            };
            button2.ValueChanged += (s, e) =>
            {
                if (button2.Value != null)
                    callback1((int)button2.Value);
            };
        }


        public static ComboBox createComboBox(Grid parent,  int row, int column, List<ListItem> items, Action<int, ComboBox> onSelectedIndexChanged, int defaultSelected = 0)
        {
            var combo = new ComboBox()
            {
                GridColumn = column,
                GridRow = row,
            };
            foreach (var item in items)
                combo.Items.Add(item);

            combo.SelectedIndex = defaultSelected;
            combo.SelectedIndexChanged += (e, s) =>
            {
                if (combo.SelectedIndex != null)
                    onSelectedIndexChanged((int)combo.SelectedIndex, combo);
            };

            parent.AddChild(combo);
            return combo;
        }

        public static CheckBox createCheckBox(Grid parent, string name, int row, int buttonColumn = 1) 
        {
            var label = new Label()
            {
                Text = name,
                GridRow = row,
                GridColumn = buttonColumn - 1,
            };
            parent.Widgets.Add(label);

            // Spin button
            var button = new CheckBox()
            {
                GridColumn = buttonColumn,
                GridRow = row,
            };
            parent.Widgets.Add(button);
            return button;
        }

        public static EventHandler<ValueChangedEventArgs<float?>> createFloatEventHandler(
            Action<float> callback)
        {
            return new EventHandler<ValueChangedEventArgs<float?>>((s, v) =>
            {
                var value = v.NewValue;
                if (value != null)
                    callback((float)value);
            });
        }

        public void draw()
        {
            desktop.Render();
        }

        public static List<ListItem> convertDictionaryToList<T>(Dictionary<string, T> dict)
        {
            List<ListItem> items = new List<ListItem>();

            foreach (var widget in dict)
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

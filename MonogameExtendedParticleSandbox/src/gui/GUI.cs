using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Particles.Profiles;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.ColorPicker;
using Myra.Graphics2D.UI.File;
using Myra.Utility;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class GUI : Exportable
    {
        private static Desktop desktop;

        private GridSizeHolder gridSizeHolder = new GridSizeHolder();
        private Grid topGrid;

        private DeletableWidgetList emitters;
        private BlendStateSelectorWidget blendStateSelector;
        private ParticleEffectValuesWidget particleEffectValues;
        private ClearColorPickerWidget clearColorPicker;

        private ScrollViewer scrollViewer;

        private Dictionary<string, ParticleEmitterWidget> particleEmitters =
            new Dictionary<string, ParticleEmitterWidget>()
            {
                { "Default", new ParticleEmitterWidget() }
            };

        private ParticleController controller;

        public GUI(ParticleController controller)
        {
            this.controller = controller;
            topGrid = new Grid();
            topGrid.ColumnsProportions.Add(new Proportion());
            topGrid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < 4; i++)
                topGrid.RowsProportions.Add(new Proportion());
            gridSizeHolder.ColumnCount++;

            blendStateSelector = new BlendStateSelectorWidget(topGrid, gridSizeHolder);
            clearColorPicker = new ClearColorPickerWidget(topGrid, gridSizeHolder);
            particleEffectValues = new ParticleEffectValuesWidget(topGrid, gridSizeHolder, controller);

            var resetButton = new TextButton()
            {
                Text = "Reset",
                GridRow = gridSizeHolder.RowCount++,
                GridColumn = 0
            };
            resetButton.TouchDown += (s, e) =>
            {
                controller.reset();
            };
            topGrid.AddChild(resetButton);

            emitters = new DeletableWidgetList(topGrid, "Create Emitter", gridSizeHolder,
                convertDictionaryToList(particleEmitters),
                (grid, i, arg3) =>
                {
                    topGrid.RowsProportions.Add(new Proportion());
                    var holder = new GridSizeHolder();
                    holder.RowCount = i;
                    holder.ColumnCount = gridSizeHolder.ColumnCount;
                    return new ParticleEmitterWidget(controller, grid, holder);
                });

            // scrollviewer that contains all the content widgets
            scrollViewer = new ScrollViewer();
            scrollViewer.Content = topGrid;

            // file menu
            var menuExport = new MenuItem()
            {
                Text = "Export to c#",
                ShortcutText = "Ctrl+E"
            };
            menuExport.Selected += (s, e) =>
            {
                var dialog = new FileDialog(FileDialogMode.SaveFile)
                {
                };
                dialog.ShowModal(GUI.desktop);

                dialog.Closed += (s, e) =>
                {
                    if (dialog.Result)
                    {
                        var code = export();
                        Exportable.writeToFile(code, dialog.FilePath);
                    }
                };
            };

            var menuFile = new MenuItem()
            {
                Text = "File"
            };
            menuFile.Items.Add(menuExport);

            var menu = new HorizontalMenu()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            menu.Items.Add(menuFile);

            // containing panel
            var pane = new VerticalStackPanel();
            pane.Width = 500;
            pane.Background = new SolidBrush("#3b3852");
            pane.Widgets.Add(menu);
            pane.Widgets.Add(scrollViewer);


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

        public static SpinButton createSpinButton(Grid parent, string name, int row, int buttonColumn = 1, bool showLable = true, int decimalPlaces = 2)
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
                    colorPicker.ShowModal(GUI.desktop);
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
                    callback2((int)button2.Value);
            };
        }

        public bool isMouseInsideUI()
        {
            return scrollViewer.IsMouseInside;
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

        public string export()
        {
            Exportable.startExport();
            var data = $@"
                    var {Exportable.EFFECT_NAME} = new ParticleEffect(autoTrigger: false)
                    {{
                        Position = new Vector2({controller.getEffect().Position.X}, {controller.getEffect().Position.Y}),
                        Emitters = new List<ParticleEmitter>
                        {{
                            {emitters.export()}
                        }}
                    }};
                   ";
            var textures = Exportable.generateTextureRegions();
            return textures + data;
        }
    }
}

using System;
using System.Collections.Generic;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;
using Myra.Utility;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class GUI
    {
        private Desktop desktop;
        private Dictionary<string, SpinButton> spinButtons = new Dictionary<string, SpinButton>();

        public GUI(ParticleController controller)
        {
            var pane = new HorizontalSplitPane();
            var scrollViewer = new ScrollViewer();
            pane.Widgets.Add(scrollViewer);

            var emmiterWidget = new ParticleEmitterWidget(controller, scrollViewer, Profile.BoxUniform(100, 100));

            // Add it to the desktop
            desktop = new Desktop();
            desktop.Root = pane;
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

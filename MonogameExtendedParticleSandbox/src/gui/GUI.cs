﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
    }
}

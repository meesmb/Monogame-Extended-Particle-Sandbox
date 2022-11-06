using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class HueInterpolatorWidget : InterpolatorWidget
    {

        private readonly float DEFAULT_START_VALUE = 0;
        private readonly float DEFAULT_END_VALUE = 360;
        private HueInterpolator i;

        private HueInterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators) : base(parent, row, interpolators)
        {
            i = new HueInterpolator()
            {
                StartValue = DEFAULT_START_VALUE,
                EndValue = DEFAULT_END_VALUE
            };
            this.interpolator = i;
            interpolators.Add(interpolator);

            var text = buildLabel(parent, "Hue", row);
            var grid = buildGrid(parent, row, 2, 1);

            var startVal = GUI.createSpinButton(grid, "StartValue", 0);
            var endVal = GUI.createSpinButton(grid, "StartValue", 1);

            startVal.Value = DEFAULT_START_VALUE;
            endVal.Value = DEFAULT_END_VALUE;

            startVal.ValueChanged += (sender, args) =>
            {
                if (startVal.Value != null)
                    i.StartValue = (float)startVal.Value;
            };
            endVal.ValueChanged += (sender, args) =>
            {
                if (endVal.Value != null)
                    i.EndValue = (float)endVal.Value;
            };
        }

        public HueInterpolatorWidget() : base(null, 0, null)
        {
        }

        public override InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new HueInterpolatorWidget(parent, row, interpolators);
        }

        public override string export()
        {
            return $@"
                    new HueInterpolatorWidget()
                    {{
                        StartValue = {i.StartValue},
                        VelocityColor = {i.EndValue},
                    }},
                    ";
        }
    }
}

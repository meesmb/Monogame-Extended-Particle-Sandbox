using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class OpacityInterpolatorWidget : InterpolatorWidget
    {
        private readonly float DEFAULT_START_VALUE = 0;
        private readonly float DEFAULT_END_VALUE = 90;


        private OpacityInterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators) : base(parent, row, interpolators)
        {
            var interpolator = new OpacityInterpolator()
            {
                StartValue = DEFAULT_START_VALUE / 100,
                EndValue = DEFAULT_END_VALUE / 100
            };
            this.interpolator = interpolator;
            interpolators.Add(interpolator);

            var text = buildLabel(parent, "Opacity", row);
            var grid = buildGrid(parent, row, 2, 1);

            var startVal = GUI.createSpinButton(grid, "StartValue / 100", 0);
            var endVal = GUI.createSpinButton(grid, "StartValue / 100", 1);

            startVal.Value = DEFAULT_START_VALUE;
            endVal.Value = DEFAULT_END_VALUE;

            startVal.ValueChanged += (sender, args) =>
            {
                if (startVal.Value != null)
                    interpolator.StartValue = (float)startVal.Value / 100;
            };
            endVal.ValueChanged += (sender, args) =>
            {
                if (endVal.Value != null)
                    interpolator.EndValue = (float)endVal.Value / 100;
            };
        }

        public OpacityInterpolatorWidget() : base(null, 0, null)
        {
        }

        public override InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new OpacityInterpolatorWidget(parent, row, interpolators);
        }
    }
}

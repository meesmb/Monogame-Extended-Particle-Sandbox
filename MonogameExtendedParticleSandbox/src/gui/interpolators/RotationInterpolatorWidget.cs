using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class RotationInterpolatorWidget : InterpolatorWidget
    {
        private readonly float DEFAULT_START_VALUE = 0;
        private readonly float DEFAULT_END_VALUE = 5;


        private RotationInterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators) : base(parent, row, interpolators)
        {
            var interpolator = new RotationInterpolator()
            {
                StartValue = DEFAULT_START_VALUE,
                EndValue = DEFAULT_END_VALUE
            };
            this.interpolator = interpolator;
            interpolators.Add(interpolator);

            var text = buildLabel(parent, "Rotation", row);
            var grid = buildGrid(parent, row, 2, 1);

            var startVal = GUI.createSpinButton(grid, "StartValue", 0);
            var endVal = GUI.createSpinButton(grid, "StartValue", 1);

            startVal.Value = DEFAULT_START_VALUE;
            endVal.Value = DEFAULT_END_VALUE;

            startVal.ValueChanged += (sender, args) =>
            {
                if (startVal.Value != null)
                    interpolator.StartValue = (float)startVal.Value;
            };
            endVal.ValueChanged += (sender, args) =>
            {
                if (endVal.Value != null)
                    interpolator.EndValue = (float)endVal.Value;
            };
        }

        public RotationInterpolatorWidget() : base(null, 0, null)
        {
        }

        public override InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new RotationInterpolatorWidget(parent, row, interpolators);
        }
    }
}

using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Particles.Modifiers.Interpolators;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class ColorInterpolatorWidget : InterpolatorWidget
    {

        private readonly Color DEFAULT_START_VALUE = Color.Yellow;
        private readonly Color DEFAULT_END_VALUE = Color.Blue;


        private ColorInterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators) : base(parent, row, interpolators)
        {
            var interpolator = new ColorInterpolator()
            {
                StartValue = DEFAULT_START_VALUE.ToHsl(),
                EndValue = DEFAULT_END_VALUE.ToHsl()
            };
            this.interpolator = interpolator;
            interpolators.Add(interpolator);

            var text = buildLabel(parent, "Color", row);
            var grid = buildGrid(parent, row, 2, 1);

            var picker1 = GUI.createColorPicker(grid, "StartValue", 0, 0, DEFAULT_START_VALUE, (hsl) =>
            {
                interpolator.StartValue = hsl;
            });

            var picker2 = GUI.createColorPicker(grid, "EndValue", 1, 0, DEFAULT_END_VALUE, (hsl) =>
            {
                interpolator.EndValue = hsl;
            });
        }

        public ColorInterpolatorWidget() : base(null, 0, null)
        {
        }

        public override InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new ColorInterpolatorWidget(parent, row, interpolators);
        }
    }
}

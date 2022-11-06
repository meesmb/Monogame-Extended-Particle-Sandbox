using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class ScaleInterpolatorWidget : InterpolatorWidget
    {

        private readonly Vector2 DEFAULT_START_VALUE = new Vector2(1, 1);
        private readonly Vector2 DEFAULT_END_VALUE = new Vector2(10, 10);
        private ScaleInterpolator i;

        private ScaleInterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators) : base(parent, row, interpolators)
        {
            i = new ScaleInterpolator()
            {
                StartValue = DEFAULT_START_VALUE,
                EndValue = DEFAULT_END_VALUE
            };
            this.interpolator = i;
            interpolators.Add(interpolator);

            var text = buildLabel(parent, "Scale", row);
            var grid = buildGrid(parent, row, 2, 1);

            GUI.createDualSpinButton(grid, 0, 0, "StartValue", (v) =>
            {
                i.StartValue = new Vector2(v, i.StartValue.X);
            }, (v) =>
            {
                i.StartValue = new Vector2(i.StartValue.Y, v);

            }, (int)DEFAULT_START_VALUE.X, (int)DEFAULT_START_VALUE.Y);
            GUI.createDualSpinButton(grid, 1, 0, "EndValue", (v) =>
            {
                i.EndValue = new Vector2(v, i.EndValue.X);
            }, (v) =>
            {
                i.EndValue = new Vector2(i.EndValue.Y, v);

            }, (int)DEFAULT_END_VALUE.X, (int)DEFAULT_END_VALUE.Y);
        }

        public ScaleInterpolatorWidget() : base(null, 0, null)
        {
        }

        public override InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new ScaleInterpolatorWidget(parent, row, interpolators);
        }

        public override string export()
        {
            return $@"
                    new ScaleInterpolator()
                    {{
                        StartValue = new Vector2({i.StartValue.X}, {i.StartValue.Y}),
                        VelocityColor = new Vector2({i.EndValue.X}, {i.EndValue.Y}),
                    }},
                    ";
        }
    }
}

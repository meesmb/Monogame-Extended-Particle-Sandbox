using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.ColorPicker;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class VelocityColorModifierWidget : ModifierWidget
    {

        private readonly Color DEFAULT_STATIONARY_COLOR = Color.Green;
        private readonly Color DEFAULT_VELOCITY_COLOR = Color.Blue;
        private readonly float DEFAULT_VELOCITY_THRESHOLD = 80;

        private VelocityColorModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new VelocityColorModifier
            {
                StationaryColor = DEFAULT_STATIONARY_COLOR.ToHsl(),
                VelocityColor = DEFAULT_VELOCITY_COLOR.ToHsl(),
                VelocityThreshold = DEFAULT_VELOCITY_THRESHOLD,
            };

            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Velocity Color", row);
            var grid = buildGrid(parent, row, 3, 1);

            var picker1 = GUI.createColorPicker(grid, "Stationary Color", 0, 0, DEFAULT_STATIONARY_COLOR, (hsl) =>
            {
                modifier.StationaryColor = hsl;
            });

            var picker2 = GUI.createColorPicker(grid, "Velocity Color", 1, 0, DEFAULT_VELOCITY_COLOR, (hsl) =>
            {
                modifier.VelocityColor = hsl;
            });

            var velocityThreshold = GUI.createSpinButton(grid, "Velocity Threshold", 2);
            velocityThreshold.ValueChanged += (s, e) =>
            {
                modifier.VelocityThreshold = (float)velocityThreshold.Value;
            };
            velocityThreshold.Value = DEFAULT_VELOCITY_THRESHOLD;
        }

        public VelocityColorModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new VelocityColorModifierWidget(parent, row, emitter);
        }
    }
}

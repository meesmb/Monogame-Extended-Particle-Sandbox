using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class VortexModifierWidget : ModifierWidget
    {
        private VortexModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new VortexModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Vortex", row);
            var grid = buildGrid(parent, row, 2, 1);

            var mass = GUI.createSpinButton(grid, "Mass", 0);
            var maxSpeed = GUI.createSpinButton(grid, "Max Speed", 1);
            var posx = GUI.createSpinButton(grid, "Position", 2);
            var posy = GUI.createSpinButton(grid, "Position", 2, 2, false);

            posx.ValueChanged += (e, s) =>
            {
                var old = modifier.Position;
                if (s.NewValue != null)
                {
                    old.X = (int)s.NewValue;
                    modifier.Position = old;
                }
            };
            posy.ValueChanged += (e, s) =>
            {
                var old = modifier.Position;
                if (s.NewValue != null)
                {
                    old.Y = (int)s.NewValue;
                    modifier.Position = old;
                }
            };
            mass.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Mass = (int)s.NewValue;
            };
            maxSpeed.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.MaxSpeed = (float)s.NewValue;
            };

        }

        public VortexModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new VortexModifierWidget(parent, row, emitter);
        }
    }
}

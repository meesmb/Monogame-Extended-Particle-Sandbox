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
        private VortexModifier m;
        private VortexModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            m = new VortexModifier();
            this.modifier = m;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Vortex", row);
            var grid = buildGrid(parent, row, 2, 1);

            var mass = GUI.createSpinButton(grid, "Mass", 0);
            var maxSpeed = GUI.createSpinButton(grid, "Max Speed", 1);
            var posx = GUI.createSpinButton(grid, "Position", 2);
            var posy = GUI.createSpinButton(grid, "Position", 2, 2, false);

            posx.ValueChanged += (e, s) =>
            {
                var old = m.Position;
                if (s.NewValue != null)
                {
                    old.X = (int)s.NewValue;
                    m.Position = old;
                }
            };
            posy.ValueChanged += (e, s) =>
            {
                var old = m.Position;
                if (s.NewValue != null)
                {
                    old.Y = (int)s.NewValue;
                    m.Position = old;
                }
            };
            mass.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    m.Mass = (int)s.NewValue;
            };
            maxSpeed.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    m.MaxSpeed = (float)s.NewValue;
            };

        }

        public VortexModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new VortexModifierWidget(parent, row, emitter);
        }
        public override string export()
        {
            return $@"
                    new VortexModifier()
                    {{
                        Position = new Vector2({m.Position.X}, {m.Position.Y}),
                        MaxSpeed = {m.MaxSpeed},
                        Mass = {m.Mass},
                    }},
                    ";
        }
    }
}

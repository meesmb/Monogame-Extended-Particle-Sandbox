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
    public class LinearGravityModifierWidget : ModifierWidget
    {
        private LinearGravityModifier m;
        private LinearGravityModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            m = new LinearGravityModifier();
            this.modifier = m;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Linear Gravity", row);
            var grid = buildGrid(parent, row, 2, 1);

            var direction1 = GUI.createSpinButton(grid, "Direction", 0);
            var direction2 = GUI.createSpinButton(grid, "Direction", 0, 2, false);
            var strength = GUI.createSpinButton(grid, "Strength", 1);

            direction1.ValueChanged += (e, s) =>
            {
                var old = m.Direction;
                if (s.NewValue != null)
                {
                    old.X = (int)s.NewValue;
                    m.Direction = old;
                }
            };
            direction2.ValueChanged += (e, s) =>
            {
                var old = m.Direction;
                if (s.NewValue != null)
                {
                    old.Y = (int)s.NewValue;
                    m.Direction = old;
                }
            };
            strength.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    m.Strength = (int)s.NewValue;
            };

        }

        public LinearGravityModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new LinearGravityModifierWidget(parent, row, emitter);
        }

        public override string export()
        {
            return $@"
                    new CircleContainerModifier()
                    {{
                        Direction = new Vector2({m.Direction.X}, {m.Direction.Y}),
                        Strength = {m.Strength},
                    }},
                    ";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class RotationModifierWidget : ModifierWidget
    {
        private RotationModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new RotationModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Rotation", row);
            var grid = buildGrid(parent, row, 3, 1);
            

            var rotationRate = GUI.createSpinButton(grid, "Rotation Rate", 0);

            rotationRate.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.RotationRate = (float)s.NewValue;
            };
            
        }

        public RotationModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new RotationModifierWidget(parent, row, emitter);
        }
    }
}

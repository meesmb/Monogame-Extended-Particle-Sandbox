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
    public class OpacityFastFadeModifierWidget : ModifierWidget
    {
        private OpacityFastFadeModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new OpacityFastFadeModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Opacity Fast Fade", row);
        }

        public OpacityFastFadeModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new OpacityFastFadeModifierWidget(parent, row, emitter);
        }

        public override string export()
        {
            return $@"
                    new OpacityFastFadeModifier(),
                    ";
        }
    }
}

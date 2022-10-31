using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class AgeModifierWidget : ModifierWidget
    {
        private AgeModifier modifier;
        private AgeModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var t = new TextButton()
            {
                GridRow = row,
                GridColumn = 1
            };
            t.Text = "Age";
            parent.AddChild(t);

            modifier = new AgeModifier();
        }

        public AgeModifierWidget() : base(null, 0, null)
        {

        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new AgeModifierWidget(parent, row, emitter);
        }
    }
}

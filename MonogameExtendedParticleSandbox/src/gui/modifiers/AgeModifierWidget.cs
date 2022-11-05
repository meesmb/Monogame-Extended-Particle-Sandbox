using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonogameExtendedParticleSandbox.src.gui.interpolators;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class AgeModifierWidget : ModifierWidget
    {
        private InterpolatorsWidget interpolators;
        private GridSizeHolder gridSizeHolder = new GridSizeHolder();
        private AgeModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new AgeModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Interpolators", row);
            var grid = buildGrid(parent, row, 1, 1);
            grid.ShowGridLines = true;

            interpolators = new InterpolatorsWidget(grid, gridSizeHolder, modifier.Interpolators);
        }

        protected override void onDelete()
        {
            base.onDelete();
            interpolators.delete();
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

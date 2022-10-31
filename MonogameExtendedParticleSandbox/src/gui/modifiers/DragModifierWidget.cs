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
    public class DragModifierWidget : ModifierWidget
    {
        private DragModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new DragModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Drag", row);
            var grid = buildGrid(parent, row, 2, 1);

            var density = GUI.createSpinButton(grid, "Density / 100", 0);
            var dragCoefficient = GUI.createSpinButton(grid, "DragCoefficient / 100", 1);

            density.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Density = (int)s.NewValue / 100;
            };
            dragCoefficient.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.DragCoefficient = (int)s.NewValue / 100;
            };

        }

        public DragModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new DragModifierWidget(parent, row, emitter);
        }
    }
}

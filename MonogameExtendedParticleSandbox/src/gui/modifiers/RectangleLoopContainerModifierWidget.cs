using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Containers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class RectangleLoopContainerModifierWidget : ModifierWidget
    {
        private RectangleLoopContainerModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new RectangleContainerModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Rectangle Loop Container", row);
            var grid = buildGrid(parent, row, 2, 1);

            var width = GUI.createSpinButton(grid, "Width", 0);
            var height = GUI.createSpinButton(grid, "Height", 1);

            width.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Width = (int)s.NewValue;
            };
            height.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Width = (int)s.NewValue;
            };

        }

        public RectangleLoopContainerModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new RectangleLoopContainerModifierWidget(parent, row, emitter);
        }
    }
}

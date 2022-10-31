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
    public class RectangleContainerModifierWidget : ModifierWidget
    {
        private RectangleContainerModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new RectangleContainerModifier();
            this.modifier = modifier;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Rectangle", row);
            var grid = buildGrid(parent, row, 3, 1);

            var width = GUI.createSpinButton(grid, "Width", 0);
            var height = GUI.createSpinButton(grid, "Heigth", 1);
            var restitution = GUI.createSpinButton(grid, "Restitution / 100", 2);

            width.Value = 150;
            height.Value = 150;
            restitution.Value = 20;

            width.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Width = (int)s.NewValue;
            };
            height.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Height = (int)s.NewValue;
            };
            restitution.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.RestitutionCoefficient = (float)((int)s.NewValue / 100);
            };
        }

        public RectangleContainerModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new RectangleContainerModifierWidget(parent, row, emitter);
        }
    }
}

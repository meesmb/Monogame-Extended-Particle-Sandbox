using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Containers;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class CircleContainerModifierWidget : ModifierWidget
    {
        private CircleContainerModifier m;
        private CircleContainerModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            m = new CircleContainerModifier();
            this.modifier = m;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Circle Container", row);
            var grid = buildGrid(parent, row, 3, 1);

            var radius = GUI.createSpinButton(grid, "Radius", 0);
            var inside = GUI.createCheckBox(grid, "Inside", 1);
            var RestitionCoefficient = GUI.createSpinButton(grid, "Restitution", 2, 1, true, 1);

            radius.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    m.Radius = (float)s.NewValue;
            };
            inside.PressedChanged += (e, s) =>
            {
                m.Inside = inside.IsChecked;
            };
            RestitionCoefficient.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    m.RestitutionCoefficient = (float)s.NewValue;
            };
        }

        public CircleContainerModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new CircleContainerModifierWidget(parent, row, emitter);
        }
        public override string export()
        {
            return $@"
                    new CircleContainerModifier()
                    {{
                        Radius = {m.Radius},
                        Inside = {m.Inside},
                        RestitutionCoefficient = {m.RestitutionCoefficient.ToString().Replace("T", "t").Replace("F", "f")}
                    }},
                    ";
        }
    }
}

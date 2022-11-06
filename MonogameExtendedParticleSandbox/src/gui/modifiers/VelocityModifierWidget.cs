using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonogameExtendedParticleSandbox.src.gui.interpolators;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class VelocityModifierWidget : ModifierWidget
    {
        private VelocityModifier m;
        private InterpolatorsWidget interpolators;
        private GridSizeHolder gridSizeHolder = new GridSizeHolder();
        private readonly float DEFAULT_VELOCITY_THRESHOLD = 80;

        private VelocityModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            m = new VelocityModifier();
            this.modifier = m;
            emitter.Modifiers.Add(modifier);

            var text = buildLabel(parent, "Velocity", row);
            var grid = buildGrid(parent, row, 3, 1);

            var velocityThreshold = GUI.createSpinButton(grid, "Velocity Threshold", 2);
            velocityThreshold.ValueChanged += (s, e) =>
            {
                m.VelocityThreshold = (float)velocityThreshold.Value;
            };
            velocityThreshold.Value = DEFAULT_VELOCITY_THRESHOLD;
            interpolators = new InterpolatorsWidget(grid, gridSizeHolder, m.Interpolators);
        }

        public VelocityModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new VelocityModifierWidget(parent, row, emitter);
        }

        public override string export()
        {

            return $@"
                    new VelocityModifier()
                    {{
                        Interpolators = 
                        {{
                            {interpolators.export()}
                        }}
                        VelocityThreshold = {m.VelocityThreshold},
                    }},
                    ";
        }
    }
}

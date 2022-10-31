using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
{
    public class SprayProfileWidget : ProfileWidget
    {
        private Vector2 direction = new Vector2(1f, 1f);
        private int spread = 2;

        public SprayProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
            : base(parent, row, particleEmitter)
        {
            var profile = Profile.Spray(direction, spread);
            particleEmitter.Profile = profile;

            var grid = buildGrid(parent, row, 2, 2);

            var lengthButton = GUI.createSpinButton(grid, "Spread", 0);
            lengthButton.Value = spread;

            lengthButton.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                {
                    spread = (int)s.NewValue;
                    particleEmitter.Profile = Profile.Spray(direction, (int)s.NewValue);
                }
            };

            GUI.createDualSpinButton(grid, 1, 0, "Direction / 100", (v) =>
            {
                direction.X = v / 100;
                particleEmitter.Profile = Profile.Spray(direction, spread);
            }, (v) =>
            {
                direction.Y = v / 100;
                particleEmitter.Profile = Profile.Spray(direction, spread);
            }, (int)direction.X * 100, (int)direction.Y * 100);

        }

        public SprayProfileWidget()
            : base(null, 0, null)
        {

        }

        public override ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new SprayProfileWidget(parent, row, particleEmitter);
        }
    }
}

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
    public class LineProfileWidget : ProfileWidget
    {
        private Vector2 axis = new Vector2(1f, 1f);
        private int length = 150;
        public override string export()
        {
            return $@"Profile.Line({axis}, {length})";
        }
        public LineProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
            : base(parent, row, particleEmitter)
        {
            var profile = Profile.Line(axis, length);
            particleEmitter.Profile = profile;

            var grid = buildGrid(parent, row, 2, 2);

            var lengthButton = GUI.createSpinButton(grid, "length", 0);
            lengthButton.Value = length;

            lengthButton.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                {
                    length = (int)s.NewValue;
                    particleEmitter.Profile = Profile.Line(axis, (int)s.NewValue);
                }
            };

            GUI.createDualSpinButton(grid, 1, 0, "Axis", (v) =>
            {
                axis.X = v / 100;
                particleEmitter.Profile = Profile.Line(axis, length);
            }, (v) =>
            {
                axis.Y = v / 100;
                particleEmitter.Profile = Profile.Line(axis, length);
            }, (int)axis.X * 100, (int)axis.Y * 100);

        }

        public LineProfileWidget()
            : base(null, 0, null)
        {

        }

        public override ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new LineProfileWidget(parent, row, particleEmitter);
        }
    }
}

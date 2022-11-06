using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
{
    public class PointProfileWidget : ProfileWidget
    {
        public PointProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
            : base(parent, row, particleEmitter)
        {
            var profile = Profile.Point();
            particleEmitter.Profile = profile;

            var grid = buildGrid(parent, row, 3, 1);
        }

        public override string export()
        {
            return $@"Profile.Point()";
        }

        public PointProfileWidget()
            : base(null, 0, null)
        {

        }

        public override ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new PointProfileWidget(parent, row, particleEmitter);
        }
    }
}

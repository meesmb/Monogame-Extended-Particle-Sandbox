using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ModifierWidget
    {
        protected Grid parent;
        protected int row;
        protected ParticleEmitter emitter;
        public ModifierWidget(Grid parent, int row, ParticleEmitter emitter)
        {
            this.parent = parent;
            this.emitter = emitter;
            this.row = row;
        }

        public virtual ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new ModifierWidget(parent, row, emitter);
        }
    }
}

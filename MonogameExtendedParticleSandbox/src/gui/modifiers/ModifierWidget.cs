using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class ModifierWidget : DeletableListWidget
    {
        protected ParticleEmitter emitter;
        protected Modifier modifier;

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

        protected override void onDelete()
        {
            emitter.Modifiers.Remove(modifier);
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.interpolators
{
    public class InterpolatorWidget : DeletableListWidget
    {
        protected Interpolator interpolator;
        private List<Interpolator> interpolators;

        public Interpolator getInterpolator()
        {
            return interpolator;
        }

        public InterpolatorWidget(Grid parent, int row, List<Interpolator> interpolators)
        {
            this.parent = parent;
            this.row = row;
            this.interpolators = interpolators;
        }

        public virtual InterpolatorWidget create(Grid parent, int row, List<Interpolator> interpolators)
        {
            return new InterpolatorWidget(parent, row, interpolators);
        }

        protected override void onDelete()
        {
            interpolators.Remove(interpolator);
        }

    }
}

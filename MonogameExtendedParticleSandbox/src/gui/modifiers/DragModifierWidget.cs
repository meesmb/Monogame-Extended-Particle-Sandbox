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
    public class DragModifierWidget : ModifierWidget
    {
        private DragModifierWidget(Grid parent, int row, ParticleEmitter emitter) : base(parent, row, emitter)
        {
            var modifier = new DragModifier();
            emitter.Modifiers.Add(modifier);
            var grid = new Grid()
            {
                GridRow = row,
                GridColumn = 1,
            };

            grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < 2; i++)
                grid.RowsProportions.Add(new Proportion());
            parent.AddChild(grid);

            var density = GUI.createSpinButton(grid, "Density", 0);
            var dragCoefficient = GUI.createSpinButton(grid, "DragCoefficient", 1);

            density.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.Density = (int)s.NewValue;
            };
            dragCoefficient.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                    modifier.DragCoefficient = (int)s.NewValue;
            };

        }

        public DragModifierWidget() : base(null, 0, null)
        {
        }

        public override ModifierWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new DragModifierWidget(parent, row, emitter);
        }
    }
}

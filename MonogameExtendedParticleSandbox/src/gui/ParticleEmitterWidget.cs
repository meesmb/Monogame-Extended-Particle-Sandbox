using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ParticleEmitterWidget : DeletableListWidget
    {

        private ParticleController controller;
        private EmitterIndex index;

        private readonly int columns = 4;
        private readonly int rows = 30;

        private GridSizeHolder topLevelGridSize = new GridSizeHolder();

        private ProfilesWidget profilesWidget;
        private parametersWidget parametersWidget;
        private ModifiersWidget modifiersWidget;

        private Grid topLevelGrid;
        private Grid parent;
        private GridSizeHolder parentHolder;

        public ParticleEmitterWidget(ParticleController controller, Grid parent, GridSizeHolder parentHolder) 
        {
            this.controller = controller;
            this.parent = parent;
            this.parentHolder = parentHolder;

            topLevelGrid = new Grid()
            {
                GridColumn = parentHolder.ColumnCount,
                GridRow = parentHolder.RowCount++,
            };
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                topLevelGrid.RowsProportions.Add(new Proportion());

            parent.AddChild(topLevelGrid);


            parametersWidget = new parametersWidget(controller, topLevelGridSize, Profile.BoxUniform(100, 100), topLevelGrid, rows, columns);
            topLevelGridSize.RowCount++;

            index = parametersWidget.index;

            profilesWidget =
                new ProfilesWidget(topLevelGrid, topLevelGridSize, controller.getEmitter(index));

            modifiersWidget = new ModifiersWidget(topLevelGrid, topLevelGridSize, rows, controller, index);
        }

        public ParticleEmitterWidget()
        {

        }

        protected override void onDelete()
        {
            parametersWidget.delete();
            modifiersWidget.delete();
            profilesWidget.delete();
            controller.removeEmitter(index);
            parent.RemoveChild(topLevelGrid);
            parentHolder.RowCount--;
        }

    }
}

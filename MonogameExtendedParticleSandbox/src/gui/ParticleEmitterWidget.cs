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
    public class ParticleEmitterWidget
    {

        private ParticleController controller;
        private int index = 0;



        private readonly int columns = 4;
        private readonly int rows = 20;

        private gridSizeHolder topLevelGridSize = new gridSizeHolder();

        private ParametersWidget parametersWidget;
        private ModifiersWidget modifiersWidget;

        public ParticleEmitterWidget(ParticleController controller, ScrollViewer parent, Profile profile)
        {
            this.controller = controller;

            var topLevelGrid = new Grid()
            {
            };
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                topLevelGrid.RowsProportions.Add(new Proportion());

            parent.Content = topLevelGrid;

            parametersWidget = new ParametersWidget(controller, topLevelGridSize, profile, topLevelGrid, rows, columns);
            topLevelGridSize.RowCount++;

            var grid = parametersWidget.grid;
            index = parametersWidget.index;

            modifiersWidget = new ModifiersWidget(topLevelGrid, rows, controller, index);
            var textButton = modifiersWidget.textButton;
            var combo = modifiersWidget.combo;

            grid.Widgets.Add(combo);
            grid.Widgets.Add(textButton);
        }

        
    }
}

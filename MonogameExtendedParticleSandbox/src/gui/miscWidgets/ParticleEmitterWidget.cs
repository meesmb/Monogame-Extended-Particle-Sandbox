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
using MonogameExtendedParticleSandbox.src.gui.profiles;
using MonogameExtendedParticleSandbox.src.gui.textures;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.miscWidgets
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
        private TexturesWidget textureWidget;

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
            this.grid = topLevelGrid;
            parent.AddChild(topLevelGrid);


            parametersWidget = new parametersWidget(controller, topLevelGridSize, Profile.BoxUniform(100, 100), topLevelGrid, rows, columns);
            index = parametersWidget.index;
            topLevelGridSize.RowCount++;

            textureWidget = new TexturesWidget(controller, index, topLevelGrid, topLevelGridSize);

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

        public override string export()
        {
            return $@"
                    new ParticleEmitter({textureWidget.export()}, {parametersWidget.c.Value}, TimeSpan.FromMilliseconds({parametersWidget.ls.Value}), {profilesWidget.export()})
                    {{
                        Parameters = new ParticleReleaseParameters
                        {{
                            Speed = new Range<float>({parametersWidget.speed1.Value}, {parametersWidget.speed2.Value}),
                            Quantity = {parametersWidget.quantity.Value},
                            Rotation = new Range<float>({parametersWidget.rotation1.Value}, {parametersWidget.rotation2.Value}),
                            Scale = new Range<float>({parametersWidget.scale1.Value}, {parametersWidget.scale2.Value}),
                        }},
                        Modifiers = 
                        {{
                            {modifiersWidget.export()}
                        }}
                    }},
                    ";
        }
    }
}

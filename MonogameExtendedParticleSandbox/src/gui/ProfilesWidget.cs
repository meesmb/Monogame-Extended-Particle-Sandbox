using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonogameExtendedParticleSandbox.src.gui.profiles;
using Myra.Graphics2D.UI;
using SharpFont.PostScript;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ProfilesWidget
    {
        private Dictionary<string, ProfileWidget> profiles = new Dictionary<string, ProfileWidget>()
        {
            {"Circle", new CircleProfileWidget()},
            {"Point", new PointProfileWidget()},
            {"Box", new BoxProfileWidget()},
            {"Box Uniform", new BoxUniformProfileWidget()},
            {"Box Fill", new BoxFillProfileWidget()},
            {"Line", new LineProfileWidget()},
            {"Ring", new RingProfileWidget()},
            {"Spray", new SprayProfileWidget()}
        };

        private ProfileWidget currentWidget;
        private Grid topGrid, widgetGrid, grid;
        private Label label;
        private ComboBox combo;

        public ProfilesWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder, ParticleEmitter emitter)
        {
            topGrid = topLevelGrid;
            widgetGrid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0,
            };
            topLevelGrid.AddChild(widgetGrid);

            grid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0
            };

            label = new Label()
            {
                GridColumn = 0,
                GridRow = 0,
                Text = "Profile: "
            };
            widgetGrid.AddChild(label);

            combo = GUI.createComboBox(widgetGrid, 0, 1, GUI.convertDictionaryToList(profiles), (v, combo) =>
            {
                var key = combo.Items[v].Text;
                if (currentWidget != null)
                    currentWidget.delete();
                currentWidget = profiles[key].create(grid, 0, emitter);
            }, 0);
            currentWidget = profiles[combo.Items[0].Text].create(grid, 0, emitter);

            var rows = 10;
            grid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = 50
            });
            grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                grid.RowsProportions.Add(new Proportion());

            topLevelGrid.AddChild(grid);

        }

        public void delete()
        {
            widgetGrid.RemoveChild(label);
            widgetGrid.RemoveChild(combo);
            topGrid.RemoveChild(grid);
            topGrid.RemoveChild(widgetGrid);
        }
    }
}

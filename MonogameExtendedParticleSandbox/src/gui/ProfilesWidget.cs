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

        public ProfilesWidget(Grid topLevelGrid, gridSizeHolder topLevelGridSizeHolder, ParticleEmitter emitter)
        {
            var selectionGrid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0,
            };
            topLevelGrid.AddChild(selectionGrid);

            var grid = new Grid()
            {
                GridRow = topLevelGridSizeHolder.RowCount++,
                GridColumn = 0
            };

            var label = new Label()
            {
                GridColumn = 0,
                GridRow = 0,
                Text = "Profile: "
            };
            selectionGrid.AddChild(label);

            var combo = GUI.createComboBox(selectionGrid, 0, 1, createProfileWidgets(), (v, combo) =>
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

        private List<ListItem> createProfileWidgets()
        {
            List<ListItem> items = new List<ListItem>();

            foreach (var widget in this.profiles)
            {
                var item = new ListItem
                {
                    Text = widget.Key
                };
                items.Add(item);
            }

            return items;
        }
    }
}

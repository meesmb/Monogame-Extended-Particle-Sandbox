using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra.Graphics2D.UI;
using SharpFont.PostScript;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
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

        private WidgetSelectWidget selectWidget;

        public ProfilesWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder, ParticleEmitter emitter)
        {
            selectWidget = new WidgetSelectWidget(topLevelGrid, topLevelGridSizeHolder, GUI.convertDictionaryToList(profiles), "Profile: ",
                (grid, row, key) => profiles[key].create(grid, row, emitter));
        }

        public void delete()
        {
            selectWidget.delete();
        }
    }
}

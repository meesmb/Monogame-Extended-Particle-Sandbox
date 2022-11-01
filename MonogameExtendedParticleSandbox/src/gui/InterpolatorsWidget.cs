using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonogameExtendedParticleSandbox.src.gui.interpolators;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class InterpolatorsWidget
    {
        private Dictionary<string, InterpolatorWidget> widgetTypes = new Dictionary<string, InterpolatorWidget>()
        {
            {"Color", new ColorInterpolatorWidget()}, 
        };

        private DeletableWidgetList deletableListWidget;


        public InterpolatorsWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder, List<Interpolator> interpolators)
        {
            deletableListWidget = new DeletableWidgetList(topLevelGrid, "New", topLevelGridSizeHolder,
                GUI.convertDictionaryToList(widgetTypes),
                (grid, i, widgetName) =>
                    widgetTypes[widgetName].create(grid, i, interpolators)
                );
        }

        public void delete()
        {

        }
    }
}

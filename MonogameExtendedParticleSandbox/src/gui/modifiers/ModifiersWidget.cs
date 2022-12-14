using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.modifiers
{
    public class ModifiersWidget : Exportable
    {
        private Dictionary<string, ModifierWidget> widgetTypes = new Dictionary<string, ModifierWidget>()
        {
            {"Age", new AgeModifierWidget()}, // needs interpolators
            {"Circle", new CircleContainerModifierWidget()},
            {"Rectangle Loop", new RectangleLoopContainerModifierWidget()},
            {"Rectangle", new RectangleContainerModifierWidget()},
            {"Drag", new DragModifierWidget()},
            {"Linear Gravity", new LinearGravityModifierWidget()},
            {"Opacity Fast Fade", new OpacityFastFadeModifierWidget()},
            {"Rotation Rate", new RotationModifierWidget()},
            {"Velocity Color", new VelocityColorModifierWidget()},
            {"Velocity", new VelocityModifierWidget()}, // needs color selector (HSL) and interpolators
            {"Vortex", new VortexModifierWidget()},
        };

        private DeletableWidgetList deletableListWidget;

        public ModifiersWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder, int rows, ParticleController controller, EmitterIndex index)
        {
            deletableListWidget = new DeletableWidgetList(topLevelGrid, "New Modifier", topLevelGridSizeHolder,
                GUI.convertDictionaryToList(widgetTypes),
                (grid, i, widgetName) =>
                {
                    return widgetTypes[widgetName].create(grid, i, controller.getEmitter(index));
                });
        }

        public void delete()
        {

            deletableListWidget.delete();
        }

        public string export()
        {
            return deletableListWidget.export();
        }
    }
}

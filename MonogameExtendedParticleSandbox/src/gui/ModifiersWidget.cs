using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ModifiersWidget
    {
        private Dictionary<string, ModifierWidget> widgetTypes = new Dictionary<string, ModifierWidget>()
        {
            {"Age", new AgeModifierWidget()}, // needs interpolators
            {"Circle", new CircleContainerModifierWidget()},
            {"RectangleLoop", new RectangleLoopContainerModifierWidget()},
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

        public ModifiersWidget(Grid topLevelGrid, GridSizeHolder topLevelGridSizeHolder, int rows, ParticleController controller, int index)
        {
            deletableListWidget = new DeletableWidgetList(topLevelGrid, "New Modifier", topLevelGridSizeHolder,
                GUI.convertDictionaryToList(widgetTypes), 
                (grid, i, widgetName) => widgetTypes[widgetName].create(grid, i, controller.getEmitter(index)));
        }

        public void delete()
        {

            deletableListWidget.delete();
        }
    }
}

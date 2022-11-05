using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.textures
{
    public class TexturesWidget
    {
        private Dictionary<string, TextureWidget> textures = new Dictionary<string, TextureWidget>()
        {
            { "Single Pixel", new SinglePixelColoredTextureWidget() },
            {"Custom Texture", new CustomTextureWidget()}
        };

        private WidgetSelectWidget selectWidget;

        public TexturesWidget(ParticleController controller, EmitterIndex index, Grid topGrid, GridSizeHolder topGridSizeHolder)
        {
            TextureWidget.DEFAULT_REGION_2D = controller.getRegion();

            selectWidget = new WidgetSelectWidget(topGrid, topGridSizeHolder, GUI.convertDictionaryToList(textures),
                "Texture: ", (grid, row, key) => textures[key].create(grid, row, controller.getEmitter(index)));
        }

        public void delete()
        {
            selectWidget.delete();
        }
    }
}

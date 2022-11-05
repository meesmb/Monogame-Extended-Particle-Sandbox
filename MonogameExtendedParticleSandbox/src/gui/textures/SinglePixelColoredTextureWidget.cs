using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Particles;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MonogameExtendedParticleSandbox.src.gui.textures
{
    public class SinglePixelColoredTextureWidget : TextureWidget
    {

        public SinglePixelColoredTextureWidget(Grid parent, int row, ParticleEmitter emitter, Texture2D texture) 
            : base(parent, row, emitter, texture)
        {
            var text = buildLabel(parent, "Single Pixel", row + 1);
            var grid = buildGrid(parent, row + 1, 1, 1);
            
            var picker1 = GUI.createColorPicker(grid, "Color", 0, 0, Color.White, (hsl) =>
            {
                texture.SetData(new[] {hsl.ToRgb()});
            });
        }

        public SinglePixelColoredTextureWidget() : base(null, 0, null, null) {}

        public override TextureWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            texture = new Texture2D(Game1.getGraphicsDevice(), 1, 1);
            texture.SetData(new[] { Color.White });
            return new SinglePixelColoredTextureWidget(parent, row, emitter, texture);
        }
    }
}

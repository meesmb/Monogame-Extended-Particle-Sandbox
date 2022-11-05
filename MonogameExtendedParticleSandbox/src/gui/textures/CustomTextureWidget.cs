using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.textures
{
    public class CustomTextureWidget : TextureWidget
    {
        public CustomTextureWidget(Grid parent, int row, ParticleEmitter emitter, Texture2D texture)
            : base(parent, row, emitter, texture)
        {
            var text = buildLabel(parent, "Texture", row + 1);
            var grid = buildGrid(parent, row + 1, 1, 1);

        }

        public CustomTextureWidget() : base(null, 0, null, null) { }

        public override TextureWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            var tex = Texture2D.FromFile(Game1.getGraphicsDevice(), "Icon.bmp");
            return new CustomTextureWidget(parent, row, emitter, tex);
        }
    }
}

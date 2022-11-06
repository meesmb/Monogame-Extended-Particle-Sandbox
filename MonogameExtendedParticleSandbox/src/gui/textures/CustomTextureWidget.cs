using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.File;

namespace MonogameExtendedParticleSandbox.src.gui.textures
{
    public class CustomTextureWidget : TextureWidget
    {
        private string currentPath = "Icon.bmp";
        public CustomTextureWidget(Grid parent, int row, ParticleEmitter emitter, Texture2D texture)
            : base(parent, row, emitter, texture)
        {
            var text = buildLabel(parent, "Texture", row + 1);
            var grid = buildGrid(parent, row + 1, 1, 1);

            var dialog = GUI.createFileDialog(grid, 0, "Choose Texture", s =>
            {
                currentPath = s;
                var newTex = getTexture(s, Game1.getGraphicsDevice());
                var oldTex = texture;
                texture = newTex;
                textureRegion = new TextureRegion2D(newTex);
                oldTex.Dispose();
                emitter.TextureRegion = textureRegion;
            });
        }

        public CustomTextureWidget() : base(null, 0, null, null) { }

        public override TextureWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            var tex = Texture2D.FromFile(Game1.getGraphicsDevice(), "Icon.bmp");
            return new CustomTextureWidget(parent, row, emitter, tex);
        }

        private Texture2D getTexture(String path, GraphicsDevice device)
        {
            Texture2D texture;

            FileStream titleStream = File.OpenRead(path);
            texture = Texture2D.FromStream(device, titleStream);
            titleStream.Close();

            return texture;
        }

        public override string export()
        {
            var num = Exportable.textureNum;
            var texture = $@"
var {Exportable.TEXTURE_NAME + num} = Texture2D.FromFile(GraphicsDevice, {'\"'}{currentPath}{'\"'});
";
            var region =
                $@"var {Exportable.TEXTURE_REGION_NAME + num} = new TextureRegion2D({Exportable.TEXTURE_NAME + num})";
            Exportable.textures.Add(texture + region);

            return Exportable.TEXTURE_REGION_NAME + num;
        }
    }
}


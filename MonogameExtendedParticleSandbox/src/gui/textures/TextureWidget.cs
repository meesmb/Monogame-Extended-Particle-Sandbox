using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Particles;
using MonoGame.Extended.TextureAtlases;
using MonogameExtendedParticleSandbox.src.gui.miscWidgets;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.textures
{
    public class TextureWidget : DeletableListWidget
    {
        protected ParticleEmitter emitter;

        public static TextureRegion2D DEFAULT_REGION_2D { get; set; }

        protected TextureRegion2D textureRegion;
        protected Texture2D texture;

        public TextureWidget(Grid parent, int row, ParticleEmitter emitter, Texture2D texture)
        {
            this.parent = parent;
            this.row = row;
            this.emitter = emitter;
            this.texture = texture;
            if (texture != null)
            {
                textureRegion = new TextureRegion2D(texture);
                emitter.TextureRegion = textureRegion;
            }
        }

        public virtual TextureWidget create(Grid parent, int row, ParticleEmitter emitter)
        {
            return new TextureWidget(parent, row, emitter, null);
        }

        protected override void onDelete()
        {
            emitter.TextureRegion = DEFAULT_REGION_2D;
            texture.Dispose();
        }

        public override string export()
        {
            return "";
        }
    }
}

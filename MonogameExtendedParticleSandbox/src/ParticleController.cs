﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.TextureAtlases;

namespace MonogameExtendedParticleSandbox.src
{
    public class ParticleController
    {
        private ParticleEffect effect;
        private TextureRegion2D region;

        public ParticleController(Texture2D texture, Vector2 pos)
        {
            region = new TextureRegion2D(texture);
            effect = new ParticleEffect(autoTrigger: true)
            {
                Position = pos,
            };
        }

        public TextureRegion2D getRegion()
        {
            return region;
        }

        public EmitterIndex addEmitter(ParticleEmitter emitter)
        {
            effect.Emitters.Add(emitter);
            return new EmitterIndex(effect.Emitters.Count - 1);
        }

        public void setParticleReleaseParameters(int index, ParticleReleaseParameters parameters)
        {
            this.effect.Emitters[index].Parameters = parameters;
        }

        public void dispose()
        {
            effect.Dispose();
        }

        /// <summary>
        /// removes an emitter from the list. SHIFTS INDEXES!!!
        /// </summary>
        /// <param name="index"></param>
        public void removeEmitter(EmitterIndex index)
        {
            Console.Out.WriteLine(index.get());
            effect.Emitters[index.get()].Dispose();
            effect.Emitters.RemoveAt(index.get());
            index.delete();
        }

        public ParticleEmitter getEmitter(EmitterIndex index)
        {
            return effect.Emitters[index.get()];
        }

        public void update(GameTime gameTime, Transform2 transform)
        {
            effect.Position = transform.Position;
            effect.Scale = transform.Scale;
            effect.Rotation = transform.Rotation;

            effect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void update(GameTime gameTime)
        {
            effect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public ParticleEffect get()
        {
            return effect;
        }
    }
}

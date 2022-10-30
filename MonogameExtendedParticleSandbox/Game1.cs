using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.ViewportAdapters;
using MonogameExtendedParticleSandbox.src;
using MonogameExtendedParticleSandbox.src.gui;
using Myra;

namespace MonogameExtendedParticleSandbox
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera;
        private ParticleController particleController;
        private GUI gui;

        private readonly int SCREEN_WIDTH = 1920;
        private readonly int SCREEN_HEIGHT = 1080;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();

            base.Initialize();
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, SCREEN_WIDTH, SCREEN_HEIGHT);
            camera = new Camera(viewportAdapter, new Vector2(0, 0));


            var particleTexture = new Texture2D(GraphicsDevice, 1, 1);
            particleTexture.SetData(new[] { Color.White });

            particleController = new ParticleController(particleTexture, new Vector2(0, 0));
            buildEmitter(particleController);

            camera.setPos(new Vector2(0, 0));

            gui = new GUI(particleController);

        }

        private void buildEmitter(ParticleController controller)
        {
            
            // int i = controller.addEmitter(new ParticleEmitter(controller.getRegion(), 500,
            //     TimeSpan.FromSeconds(2.5), Profile.BoxUniform(100, 250))
            // {
            //     Parameters = new ParticleReleaseParameters
            //     {
            //         Speed = new Range<float>(0f, 50f),
            //         Quantity = 3,
            //         Rotation = new Range<float>(-1f, 1f),
            //         Scale = new Range<float>(3.0f, 4.0f)
            //     },
            //     Modifiers =
            //     {
            //         new AgeModifier
            //         {
            //             Interpolators =
            //             {
            //                 new ColorInterpolator
            //                 {
            //                     StartValue = new HslColor(0.33f, 0.5f, 0.5f),
            //                     EndValue = new HslColor(0.5f, 0.9f, 1.0f)
            //                 }
            //             }
            //         },
            //         new RotationModifier {RotationRate = -2.1f},
            //         new RectangleContainerModifier {Width = 800, Height = 480},
            //         new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
            //     }
            // });
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MyraEnvironment.Game = this;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            particleController.update(gameTime);
            camera.update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: camera.getViewMatrix());
            spriteBatch.Draw(particleController.get());
            spriteBatch.End();

            gui.draw();
        }
    }
}
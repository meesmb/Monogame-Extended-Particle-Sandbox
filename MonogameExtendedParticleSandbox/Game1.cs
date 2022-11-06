using System;
using System.Globalization;
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
using Console = System.Console;

namespace MonogameExtendedParticleSandbox
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera;
        private ParticleController particleController;
        private GUI gui;

        private static Game1 self;

        private readonly int SCREEN_WIDTH = 1920;
        private readonly int SCREEN_HEIGHT = 1080;
        public Game1()
        {
            System.Globalization.CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            self = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static GraphicsDevice getGraphicsDevice()
        {
            return self.GraphicsDevice;
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

            camera.canMove = !gui.isMouseInsideUI();

            particleController.update(gameTime);
            camera.update(gameTime);
        }

        public static BlendState blendState { get; set; } = BlendState.AlphaBlend;
        public static Color clearColor { get; set; } = Color.CornflowerBlue ;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);
            spriteBatch.Begin(SpriteSortMode.BackToFront, blendState, transformMatrix: camera.getViewMatrix());
            spriteBatch.Draw(particleController.get());
            spriteBatch.End();

            gui.draw();
        }
    }
}
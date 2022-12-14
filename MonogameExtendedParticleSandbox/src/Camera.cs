using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.ViewportAdapters;

namespace MonogameExtendedParticleSandbox.src
{
    public class Camera
    {
        private OrthographicCamera camera;
        private int lastMouseWheelValue = 0;
        private Point lastMousePosition = new Point(0, 0);

        public bool canMove { get; set; } = true;

        public Camera(ViewportAdapter adapter, Vector2 initialPos)
        {
            camera = new OrthographicCamera(adapter);
            camera.Zoom = 5f;

            camera.Position = initialPos;

            camera.MaximumZoom = 100;
            camera.MinimumZoom = 0.01f;

            this.lastMouseWheelValue = Mouse.GetState().ScrollWheelValue;
            lastMousePosition = Mouse.GetState().Position;
        }

        public void update(GameTime gameTime)
        {
            if (!canMove)
                return;

            float zoomValue = 0.1f;
            int r = (int)(camera.Zoom / 2.5);
            zoomValue += r;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                zoomValue += 1;

            if (this.lastMouseWheelValue > Mouse.GetState().ScrollWheelValue && (camera.Zoom - zoomValue) > camera.MinimumZoom + 0.01f)
            {
                camera.Zoom -= zoomValue;
            }
            if (this.lastMouseWheelValue < Mouse.GetState().ScrollWheelValue && (camera.Zoom + zoomValue) < camera.MaximumZoom - 1f)
            {
                camera.Zoom += zoomValue;
            }
            lastMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                var diff = lastMousePosition - Mouse.GetState().Position;

                camera.Move(diff.ToVector2() * (1/ camera.Zoom));
            }

            lastMousePosition = Mouse.GetState().Position;
        }

        public void move(Vector2 v)
        {
            camera.Move(v);
        }

        public Matrix getViewMatrix()
        {
            return camera.GetViewMatrix();
        }

        public Vector2 getPos()
        {
            return camera.Position;
        }

        public Vector2 getWorldPosition(Vector2 screenPosition)
        {
            screenPosition = camera.ScreenToWorld(new Vector2(screenPosition.X, screenPosition.Y));
            return screenPosition;
        }

        public Vector2 getScreenPosition(Vector2 worldPosition)
        {
            worldPosition = camera.WorldToScreen(new Vector2(worldPosition.X, worldPosition.Y));

            return worldPosition;
        }

        public void setPos(Vector2 pos)
        {
            camera.LookAt(pos);
        }
    }
}

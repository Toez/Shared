using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GravityBalls
{
    class Circle
    {
        Texture2D pixel; //our pixel texture we will be using to draw primitives
        GraphicsDevice gd; //graphics device to use
        SpriteBatch sb; //sprite batch to use

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; set; }
        public int Radius { get; private set; }
        public int Weight { get; private set; }
        private Color color;

        public Circle(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Vector2 Position, Color color, int Radius, int Weight, Vector2 Velocity)
        {
            this.gd = graphicsDevice;
            this.sb = spriteBatch;
            this.Position = Position;
            this.color = color;
            this.Radius = Radius;
            this.Weight = Weight;
            this.Velocity = Velocity;
            pixel = new Texture2D(gd, 1, 1);
            pixel.SetData(new Color[] {color});
        }

        public void Update()
        {
            Position += Velocity;
        }

        public void Draw()
        {
            for (int x = (int)Position.X - Radius; x <= (int)Position.X + Radius; x++)
            {
                for (int y = (int)Position.Y - Radius; y <= (int)Position.Y + Radius; y++)
                {
                    float dx = x - Position.X;
                    float dy = y - Position.Y;
                    if ((dx * dx + dy * dy) < (Radius * Radius))
                    {
                        sb.Draw(pixel, new Vector2(x, y), color);
                    }
                }
            }
        }
    }
}

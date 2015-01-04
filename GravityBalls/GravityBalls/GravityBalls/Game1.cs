using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GravityBalls
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Circle> Circles;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Circles = new List<Circle>();

            Circles.Add(new Circle(graphics.GraphicsDevice, spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.Yellow, 25, 15, Vector2.Zero));
            Circles.Add(new Circle(graphics.GraphicsDevice, spriteBatch, new Vector2(graphics.PreferredBackBufferWidth / 3, graphics.PreferredBackBufferHeight / 3), Color.Blue, 10, 5, new Vector2(0.5f, 0)));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Gravity
            for (int i = 0; i < Circles.Count; i++)
            {
                Circle circle1 = (Circle)Circles[i];

                for (int n = 0; n < Circles.Count; n++)
                {
                    Circle circle2 = (Circle)Circles[n];

                    if (circle2 == Circles[0])
                        continue;

                    if (circle1 != circle2 && n > 0)
                    {
                        Vector2 distance = circle1.Position - circle2.Position;
                        float r = distance.Length();
                        float force = 9.8f * ((circle2.Weight * circle1.Weight) / (r * r));
                        float acc = force / circle2.Weight;
                        distance = Vector2.Normalize(distance);
                        circle2.Velocity += Vector2.Multiply(distance, acc);
                    }

                    Circles[n] = circle2;

                    if (n > 0)
                        Circles[n].Update();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int i = 0; i < Circles.Count; i++)
                Circles[i].Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

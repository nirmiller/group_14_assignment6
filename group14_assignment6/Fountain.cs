using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace group14_assignment6
{
    public class Fountain
    {
        private class Particle
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public float Life;
            public float MaxLife;
            public Color Color;
        }

        private List<Particle> particles;
        private Random rand;

        private Vector2 origin;
        private float gravity = 400f;

        private int spawnRate; // how intense the fountain is

        private Texture2D pixel;

        // Custom color palette per fountain
        private Color[] colors;

        public Fountain(
            Vector2 origin,
            GraphicsDevice graphicsDevice,
            Color[] colors,   // pass in colors
            int spawnRate = 15 // pass in intensity
        )
        {
            this.origin = origin;
            this.colors = colors;
            this.spawnRate = spawnRate;

            particles = new List<Particle>();
            rand = new Random();

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Spawn particles
            for (int i = 0; i < spawnRate; i++)
            {
                particles.Add(CreateParticle());
            }

            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle p = particles[i];

                p.Velocity.Y += gravity * dt;
                p.Position += p.Velocity * dt;
                p.Life -= dt;

                if (p.Life <= 0)
                    particles.RemoveAt(i);
            }
        }

        private Particle CreateParticle()
        {
            float speed = rand.Next(200, 400);
            float angle = MathHelper.ToRadians(rand.Next(-25, 25));

            Vector2 velocity = new Vector2(
                (float)Math.Sin(angle) * speed,
                -(float)Math.Cos(angle) * speed
            );

            float life = (float)rand.NextDouble() * 1.5f + 0.5f;

            return new Particle
            {
                Position = origin,
                Velocity = velocity,
                Life = life,
                MaxLife = life,
                Color = GetRandomColor()
            };
        }

        private Color GetRandomColor()
        {
            return colors[rand.Next(colors.Length)];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var p in particles)
            {
                float t = p.Life / p.MaxLife;

                spriteBatch.Draw(
                    pixel,
                    p.Position,
                    null,
                    p.Color * t,
                    0f,
                    Vector2.Zero,
                    3f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
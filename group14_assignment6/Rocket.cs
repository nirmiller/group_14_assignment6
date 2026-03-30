using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group14_assignment6;

public class Rocket
{
    private Vector2 initialVelocity;
    private Vector2 rocketPosition;
    private Texture2D rocketTexture;
    private Texture2D currentTexture;
    private Texture2D rocketThrust;
    private float rocketAngle;
    private Vector2 origin;
    private Vector2 rocketVelocity;
    private Vector2 gravity = new Vector2(0, 300f);
    private bool rocketVisible;
    private float screenSize;

    private float scale = 0.06f;
    
    // public properties added so firework can spawn in correct position 
    public Vector2 Position => rocketPosition;
    public bool IsVisible => rocketVisible;

    public Rocket(Vector2 _initialVelocity, Vector2 _rocketPosition,
        Texture2D _rocketTexture,Texture2D _rocketThrust, bool _rocketVisible, float _screenSize)
    {
        initialVelocity = _initialVelocity;
        rocketPosition = _rocketPosition;
        rocketTexture = _rocketTexture;
        rocketThrust = _rocketThrust;
        rocketVisible = _rocketVisible;
        screenSize = _screenSize;

        currentTexture = _rocketThrust;
        origin = new Vector2(
            rocketTexture.Width / 2f,
            rocketTexture.Height / 2f
        );

        rocketVelocity = initialVelocity;
    }

    public void rocketDisappear()
    {
        rocketVisible = false;
    }

    public void rocketAppear()
    {
        rocketVisible = true;
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        rocketVelocity += gravity * dt;
        rocketPosition += rocketVelocity * dt;

        float halfHeight = (rocketTexture.Height * scale) / 2f;
        float groundY = screenSize;

        if (rocketPosition.Y >= groundY)
        {
            rocketPosition.Y = groundY;
            rocketVelocity.Y = 0.0f;
        }

        if (MathF.Abs(rocketVelocity.Y) <= 0.01f)
        {
            rocketVisible = false;
        }

        if (rocketVelocity != Vector2.Zero)
        {
            rocketAngle = (float)System.Math.Atan2(rocketVelocity.Y, rocketVelocity.X) + 90;
        }

        if (MathF.Abs(rocketVelocity.Y) < MathF.Abs(initialVelocity.Y/1.5f))
        {
            currentTexture = rocketTexture;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (rocketVisible)
        {
            spriteBatch.Draw(
                currentTexture,
                rocketPosition,
                null,
                Color.White,
                rocketAngle,
                origin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group14_assignment6;

public class FireworkParticles
{
    private Random _random;

    private Texture2D _fireworkParticleTexture;
    private float _randomTextureRotation;       
    
    private Vector2 _startPosition;     // same as the rocket end position
    private Vector2 _position;
    private Vector2 _velocity;
    private float _randomAngle;     // angle that the particle will go out at

    private float _alpha;       // amount of fade for particle 
    private float _maxRadius;      

    public FireworkParticles(Texture2D particleTexture,
            Vector2 rocketEndPosition,
            float maxRadius)
    
    {
        _fireworkParticleTexture = particleTexture;
        _startPosition = rocketEndPosition;
        _maxRadius = maxRadius;
        _alpha = 1f;        // starting fully opaque 

        _random = new Random();

        // mix of slower particles (clustered near center) and faster ones (burst further)
        float speed;
        double roll = _random.NextDouble();
        
        // making slower particles 
        if (roll < 0.4)
        {
            // 40% will fall into slower velocity category  
            speed = 0.5f + (float)_random.NextDouble() * 1.5f;  // 0.5 – 2.0
        }
        // making faster particles 
        else
        {
            // 60% will fall into faster velocity category 
            speed = 2.0f + (float)_random.NextDouble() * 6.0f;  // 2.0 – 8.0
        }

        _randomAngle = (float)_random.NextDouble() * MathF.PI * 2f;

        _position = rocketEndPosition;

        // using speed and randomAngle initialized above, calculating velocity in terms of the x and y component 
        _velocity = new Vector2(
            speed * MathF.Cos(_randomAngle),
            speed * MathF.Sin(_randomAngle));

        _randomTextureRotation = (MathF.PI * 2f) * (float)_random.NextDouble();
    }

    public void ApplyGravity(float gForceX, float gForceY)
    {
        _velocity.X += gForceX;
        _velocity.Y += gForceY;

        _position.X += _velocity.X;
        _position.Y += _velocity.Y;

        // fade based on how close it is to max radius 
        float dx = _position.X - _startPosition.X;
        float dy = _position.Y - _startPosition.Y;
        float distance = MathF.Sqrt(dx * dx + dy * dy);

        // Alpha starts at 1.0 --> reaches 0 at maxRadius
        float progress = distance / _maxRadius;       // 0 = at center, 1 = at edge
        progress = MathHelper.Clamp(progress, 0f, 1f);
        _alpha = 1f - (progress * progress);          // quadratic so it fades quicker as it gets closer to max radius 
    }

    public void Display(SpriteBatch sb)
    {
        if (_alpha <= 0.01f) return;  // skip drawing super transparent particles

        // applying alpha in tint- which is called in sb.Draw
        Color tint = Color.White * _alpha;
        
        sb.Draw(
            texture: _fireworkParticleTexture,
            position: _position,
            sourceRectangle: null,
            color: tint,
            rotation: _randomTextureRotation,
            origin: new Vector2(_fireworkParticleTexture.Width / 2f,
                                _fireworkParticleTexture.Height / 2f),
            scale: 0.2f,
            effects: SpriteEffects.None,
            layerDepth: 0f);
    }
}
    
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace group14_assignment6;

public class Rocket
{
    private float speed;
    private Vector2 rocketPosition;
    private Texture2D rocketTexture;
    private float rocketAngle;
    private Vector2 origin; 

    public Rocket(float _speed, float _rocketAngle, Vector2 _rocketPosition, Texture2D _rocketTexture)
    {
        speed = _speed;
        rocketPosition = _rocketPosition; 
        rocketTexture = _rocketTexture;
        rocketAngle = _rocketAngle;
        Vector2 origin = new Vector2(
            rocketTexture.Width / 2f,
            rocketTexture.Height / 2f
        );

    }


    public void Draw(SpriteBatch spriteBatch)
    {
        
        spriteBatch.Draw(
            rocketTexture,
            rocketPosition,
            null,
            Color.White,
            0f,
            origin,
            .2f,
            SpriteEffects.None,
            0f
        );
    }
    
}
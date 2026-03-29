using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace group14_assignment6;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // rocket 
    private Rocket rocket;
    private Texture2D _rocketTexture;
    private Texture2D _rocketLaunchTexture;
    
    // firework
    private List<FireworkParticles> _particleList;
    private Texture2D _particleTexture;
    private Vector2 _rocektEndPosition = new Vector2(200f, 200f);
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferHeight = 500;
        _graphics.PreferredBackBufferWidth = 800;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // rocket 
        _rocketTexture = Content.Load<Texture2D>("imgs/rocket_high_res");
        _rocketLaunchTexture =  Content.Load<Texture2D>("imgs/rocket_high_res_thrust");
        rocket = new Rocket(new Vector2(50f, -450f), new Vector2(400, 460), _rocketTexture ,_rocketLaunchTexture, true, _graphics.PreferredBackBufferHeight);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // firework 
        _particleTexture = Content.Load<Texture2D>("imgs/purpleParticle");
        _particleList = new List<FireworkParticles>();
        
        for (int i = 0; i < 200; i++)
        {
            _particleList.Add(new FireworkParticles(_particleTexture,
                _rocektEndPosition,
                100f));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // rocket 
        rocket.Update(gameTime);
        
        // firework
        foreach (FireworkParticles particle in _particleList)
        {
            particle.ApplyGravity(0f, 0.02f);

        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        // rocket 
        rocket.Draw(_spriteBatch);
        
        // firework
        foreach (FireworkParticles particle in _particleList)
        {
            particle.Display(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group14_assignment6;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Rocket rocket;

    private Texture2D _rocketTexture;
    private Texture2D _rocketLaunchTexture;
    
    private Fountain fountain1;
    private Fountain fountain2;
    private Fountain fountain3;
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
        
        // positions of fountains
        Vector2 pos1 = new Vector2(200, 500);
        Vector2 pos2 = new Vector2(400, 500);
        Vector2 pos3 = new Vector2(600, 500);

        // colors of fountains
        Color[] colors1 = { Color.Orange, Color.Yellow, Color.Red };
        Color[] colors2 = { Color.Cyan, Color.Blue, Color.LavenderBlush };
        Color[] colors3 = { Color.Lime, Color.Green, Color.YellowGreen };

        // intensity (spawn rate) of fountains
        int intensity1 = 12;
        int intensity2 = 18;
        int intensity3 = 25;

        // Create fountains
        fountain1 = new Fountain(pos1, GraphicsDevice, colors1, intensity1);
        fountain2 = new Fountain(pos2, GraphicsDevice, colors2, intensity2);
        fountain3 = new Fountain(pos3, GraphicsDevice, colors3, intensity3);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        
        _rocketTexture = Content.Load<Texture2D>("imgs/rocket_high_res");
        _rocketLaunchTexture =  Content.Load<Texture2D>("imgs/rocket_high_res_thrust");
        rocket = new Rocket(new Vector2(50f, -450f), new Vector2(400, 460), _rocketTexture ,_rocketLaunchTexture, true, _graphics.PreferredBackBufferHeight);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        rocket.Update(gameTime);
       
        
        fountain1.Update(gameTime);
        fountain2.Update(gameTime);
        fountain3.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        
        _spriteBatch.Begin();
        rocket.Draw(_spriteBatch);
        
        fountain1.Draw(_spriteBatch);
        fountain2.Draw(_spriteBatch);
        fountain3.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
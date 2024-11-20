using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace topic_4_monogame_sounds
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture;
        Rectangle bombRect;

        Texture2D explosionTexture;
        Rectangle explosionRect;

        SpriteFont Timefont;
        float seconds;

        SoundEffect explosionSound;
        SoundEffectInstance explodeInstance;

        MouseState mouseState;

        bool done;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here

            base.Initialize();

            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(0, 0, 800, 500);

            _graphics.PreferredBackBufferWidth = 800; 
            _graphics.PreferredBackBufferHeight = 500; 
            _graphics.ApplyChanges();

            done = false;

            seconds = 5;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Timefont = Content.Load<SpriteFont>("Timefont");

            bombTexture = Content.Load<Texture2D>("bomb");


            explosionSound = Content.Load<SoundEffect>("explosion");
            explosionTexture = Content.Load<Texture2D>("explosionImage");


            explodeInstance = explosionSound.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (!done)
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds < 0)
            {
                seconds = 0;
                explodeInstance.Play();
                done = true;
            }

            if (done)
            {
                if (explodeInstance.State == SoundState.Stopped) 
                    Exit();
            }
 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(Timefont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            if (seconds <= 0)
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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

        Texture2D cuttersTexture;
        Rectangle cuttersRect;

        SpriteFont Timefont;
        float seconds;



        Rectangle greenRect, greenRect2, redRect, redRect2, redRect3;

        SoundEffect explosionSound;
        SoundEffect cheeringSound;
        SoundEffectInstance cheeringInstance;
        SoundEffectInstance explodeInstance;


        MouseState mouseState;

        bool done;
        bool sound;
        bool cheer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here

            base.Initialize();

            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(0, 0, 800, 500);
            cuttersRect = new Rectangle(0, 0, 100, 100);

            greenRect = new Rectangle(490, 160, 110, 15);
            greenRect2 = new Rectangle(680, 180, 30, 60);
            redRect = new Rectangle(490, 180, 100, 25);
            redRect2 = new Rectangle(670, 170, 80, 10);
            redRect3 = new Rectangle(715, 180, 35, 75);


            _graphics.PreferredBackBufferWidth = 800; 
            _graphics.PreferredBackBufferHeight = 500; 
            _graphics.ApplyChanges();

            done = false;
            sound = false;
            cheer = false;

            seconds = 15;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Timefont = Content.Load<SpriteFont>("Timefont");

            bombTexture = Content.Load<Texture2D>("bomb");

            cuttersTexture = Content.Load<Texture2D>("cutters");


            explosionSound = Content.Load<SoundEffect>("explosion");
            cheeringSound = Content.Load<SoundEffect>("cheering");
            explosionTexture = Content.Load<Texture2D>("explosionImage");


            explodeInstance = explosionSound.CreateInstance();
            cheeringInstance = cheeringSound.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            cuttersRect.Location = mouseState.Position;
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (!done)
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds < 0)
            {
                seconds = 0;
                explodeInstance.Play();
                done = true;
                sound = true;
            }

            if (sound)
            {
                if (explodeInstance.State == SoundState.Stopped) 
                    Exit();
            }

            if (cheer)
                if(cheeringInstance.State == SoundState.Stopped)
                    Exit();
                

            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                if (redRect.Contains(mouseState.Position))
                {
                    seconds = 0;
                    explodeInstance.Play();
                    done = true;
                    sound = true;
                }
                
                if (redRect2.Contains(mouseState.Position))
                {
                    seconds = 0;
                    explodeInstance.Play();
                    done = true;
                    sound = true;
                }
                if (redRect3.Contains(mouseState.Position))
                {
                    seconds = 0;
                    explodeInstance.Play();
                    done = true;
                    sound = true;
                }

                if (greenRect.Contains(mouseState.Position))
                {
                    done = true;
                    cheeringInstance.Play();
                    cheer = true;
                }


                if (greenRect2.Contains(mouseState.Position))
                {
                    done = true;
                    cheeringInstance.Play();
                    cheer = true;
                }

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
            _spriteBatch.Draw(cuttersTexture, cuttersRect, Color.White);
            _spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}

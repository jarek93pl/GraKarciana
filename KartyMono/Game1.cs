using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Komputer.Xna.Menu;
using KartyMono.Menu;
using Microsoft.Xna.Framework.Content;

namespace KartyMono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Game game;
        MenuPodstawa menu;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager ContentStatic;
        public static Texture2D Cursor;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;
            game = this;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        public static void SetTitle(string s)
        {
            game.Window.Title = s;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentStatic = this.Content;
            Cursor = Content.Load<Texture2D>("table/m");
            GlobalStaticDate.Text = Content.Load<SpriteFont>("Font");
            menu = new Menu1000Game(Content);
        }
        
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            menu?.UpDate(gameTime);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);
            
            spriteBatch.Begin();
            menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private static Texture2D rect;
        public static void DrawRectangle(Rectangle coords, Color color,SpriteBatch sp)
        {
            if (rect == null)
            {
                rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }
            sp.Draw(rect, coords, color);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shitemon
{
    using BattleSystem;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix scale;
        KeyboardState ks, p_ks;

        BattleSystem.BattleSystem bs;
        MessageBox msgbox;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            // Somewhere accessible
            const int TargetWidth = 638;
            const int TargetHeight = 480;

            // Somewhere in initialisation
            float scaleX = graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / TargetHeight;
            scale = Matrix.CreateScale(new Vector3(scaleX * 2, scaleY * 2, 1));

            graphics.PreferredBackBufferWidth = TargetWidth;
            graphics.PreferredBackBufferHeight = TargetHeight;
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
            bs = new BattleSystem.BattleSystem(Content);



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


            msgbox = new MessageBox(
                Content.Load<Texture2D>("ui/msgbox"),
                Content.Load<Texture2D>("ui/cursor"),
                Content.Load<SpriteFont>("fonts/text"));

            var tex = Content.Load<Texture2D>("sprites/mon/planirt");

            var player = new Mon("Planirt", tex, new Stats(30, 8, 6, 5), "Grass", string.Empty);
            var enemy = new Mon("Planirt", tex, new Stats(30, 8, 6, 5), "Grass", string.Empty);

            bs.Initialize(player, enemy);

            player.AssignMoves("Shock", "Electric Bolt", "Lightning Bolt", "Thunder");
            enemy.AssignMoves("Shock", "Electric Bolt", "Lightning Bolt", "Thunder");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void Menu_BackToDefault()
        {
            msgbox.menu_current = "main";
            msgbox.menu_index = 0;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            p_ks = ks;
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Escape))
                Exit();


            if(ks.IsKeyDown(Keys.D) && p_ks.IsKeyUp(Keys.D))
            {
                msgbox.menu_index += 1;
                if (msgbox.menu_index == 4)
                    msgbox.menu_index = 0;
            }
            else if (ks.IsKeyDown(Keys.A) && p_ks.IsKeyUp(Keys.A))
            {
                msgbox.menu_index -= 1;
                if (msgbox.menu_index == -1)
                    msgbox.menu_index = 3;
            }

            if (ks.IsKeyDown(Keys.Q) && p_ks.IsKeyUp(Keys.Q))
            {
                if (msgbox.menu_current == "moves")
                {
                    Menu_BackToDefault();
                }
            }
            if (ks.IsKeyDown(Keys.Space) && p_ks.IsKeyUp(Keys.Space))
            {
                if(msgbox.menu_current == "main" 
                    && msgbox.menu_index == 0) // moves menu index
                {
                    msgbox.menu_current = "moves";
                }
                else if (msgbox.menu_current == "moves")
                {
                    if(bs.UseMove(msgbox.menu_index))
                    {
                        bs.SetMoveAnim(Content.Load<Texture2D>("sprites/moves/shock"));
                    }

                    // anim of move
                    // resolve effect of move
                    // enemy use move
                    // anim of enemy move
                    // resolve effect of move
                    // resolve effects

                    // set everything back to main menu and choice 0
                    Menu_BackToDefault();
                }
            }


            bs.Update((float)gameTime.ElapsedGameTime.TotalSeconds);





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, scale);

            bs.Render(spriteBatch, msgbox);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

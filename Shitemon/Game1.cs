using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shitemon
{
    using BattleSystem;
    using Microsoft.Xna.Framework.Audio;
    using System;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class Game1 : Game
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


            var hp_tex = Content.Load<Texture2D>("ui/healthbar");
            var r_player_healthbar = new Rectangle(10, 70, hp_tex.Width, hp_tex.Height);
            var r_enemy_healthbar = new Rectangle(200, 20, hp_tex.Width, hp_tex.Height);

            MonRenderData player_renderData = new MonRenderData(tex, 
                new Rectangle(50, 80, 64, 64), 
                new Rectangle(0, 0, 64, 64), hp_tex, r_player_healthbar);
            MonRenderData enemy_renderData = new MonRenderData(tex, 
                new Rectangle(200, 30, 64, 64), 
                new Rectangle(0, 0, 64, 64), hp_tex, r_enemy_healthbar);

            var player = new Mon("Planirt", player_renderData, new Stats(100, 8, 6, 5), TYPECHART.Grass, TYPECHART.None);
            var enemy = new Mon("Enemy Planirt", enemy_renderData, new Stats(100, 8, 6, 5), TYPECHART.Grass, TYPECHART.None);

            bs.Initialize(player, enemy);

            player.AssignMoves("Shock", "Discharge", "", "");
            enemy.AssignMoves("Shock", "", "", "");
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

            // Here we set to 4 again because moves might have set it to something lower
            msgbox.SetMaxIndex(4);

            msgbox.ResetMenuIndex();
        }




        void MessageBox_UsedMove(object sender, EventArgs e)
        {
            var o = (MoveQueueObj)sender;

            SoundEffect sfx;
            sfx = Content.Load<SoundEffect>("sound/electric");
            sfx.Play();

            Console.WriteLine("Playing sfx...");

            msgbox.menu_current = "text";

            float m = Utils.GetTypechartModifier(o.Move, o.Target, out string message);

            msgbox.text = string.Format("{0} used {1}!\n{2}!", o.User.name, o.Move.name, message);
        }

        void MessageBox_BackToDefault(object sender, EventArgs e)
        {
            Menu_BackToDefault();
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

            if (bs.GetMoveQueueCount() == 0)
            {
                if (ks.IsKeyDown(Keys.D) && p_ks.IsKeyUp(Keys.D))
                {
                    msgbox.NextMenuIndex();
                }
                else if (ks.IsKeyDown(Keys.A) && p_ks.IsKeyUp(Keys.A))
                {
                    msgbox.PrevMenuIndex();
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
                    // Get current selected menu item index.
                    int menu_index = msgbox.GetMenuIndex();

                    if (msgbox.menu_current == "main" && menu_index == 0) // menu index of moves
                    {
                        // If here, moves was selected in main menu and therefore we prepare
                        // for the next frame where moves are rendered.
                        msgbox.menu_current = "moves";

                        msgbox.SetMaxIndex(bs.GetPlayer().GetAssignedMovesCount());
                    }
                    else if (msgbox.menu_current == "moves")
                    {
                        var player = bs.GetPlayer();
                        var enemy = bs.GetEnemy();

                        bs.SetState(BattleSystem.BattleSystem.BATTLE_PHASES.Speed_Calc);

                        bool player_first = false;

                        void QueuePlayer()
                        {
                            // Player use move
                            var q = bs.QueueMove(player.GetMoves()[menu_index], player, enemy, this.Content);

                            q.AnimStarted += MessageBox_UsedMove;
                            q.Invoked += MessageBox_BackToDefault;
                        }

                        void QueueEnemy()
                        {
                            // Enemy use move
                            var rng_i_max = enemy.GetAssignedMovesCount();
                            var rng_i = new Random().Next(0, rng_i_max); // enemy use 0-rng_i_max

                            var q = bs.QueueMove(enemy.GetMoves()[rng_i], enemy, player, this.Content);

                            q.AnimStarted += MessageBox_UsedMove;
                            q.Invoked += MessageBox_BackToDefault;
                        }


                        if (player.stats.speed == enemy.stats.speed) // 50/50 to who strikes first
                        {
                            int rng_mod = new Random().Next(0, 2); // 0 or 1
                            if (rng_mod == 1)
                            {
                                player_first = true;
                            }
                        }
                        else if (player.stats.speed > enemy.stats.speed) // Player is faster
                        {
                            player_first = true;
                        }
                        else
                            player_first = false;


                        bs.SetState(BattleSystem.BattleSystem.BATTLE_PHASES.Select_Moves);
                        if (player_first)
                        {
                            QueuePlayer();
                            QueueEnemy();
                        }
                        else
                        {
                            QueueEnemy();
                            QueuePlayer();
                        }


                        // set everything back to main menu and choice 0
                        Menu_BackToDefault();
                    }
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Shitemon.BattleSystem
{
    // Mostly stuff that are fields or take up space and we do not need to change
    public partial class BattleSystem
    {
        Mon player;
        Mon enemy;

        public Mon GetPlayer() => player;
        public Mon GetEnemy() => enemy;

        // Animation and move and effect queue
        List<List<MoveQueueObj>> effect_que = new List<List<MoveQueueObj>>();

        SoundEffect bgm;
        SoundEffectInstance bgm_instance;
        SpriteFont font;

        public BattleSystem(ContentManager contentManager)
        {
            bgm = contentManager.Load<SoundEffect>("sound/battle");
            bgm_instance = bgm.CreateInstance();
            bgm_instance.IsLooped = true;
            bgm_instance.Play();

            font = contentManager.Load<SpriteFont>("fonts/text");
        }

        public void Initialize(Mon player, Mon enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public MoveQueueObj QueueMove(Move move, Mon user, Mon target, ContentManager contentManager)
        {
            string str = string.Format("sprites/moves/{0}", move.asset_name);


            var anim_move = new BattleAnimation(2f, contentManager.Load<Texture2D>(str), target.renderData.sprite_dest, target.renderData.sprite_rect);


            // TODO: Implement more animations
            anim_move.HookRender(MoveDelegateBank.MoveBlinkRender);
            //if(move.type == TYPECHART.Robotic)
            //{
            //    anim_move.HookRender(MoveDelegateBank.MoveBlinkRender);
            //}
            //else if (move.type == TYPECHART.Fire)
            //{

            //}


            // Invoke move delegate and save the result
            var moveResult = move.moveDelegate(user, target, move, out int damage);

            var anim_healthbar = new BattleAnimation(this, target, damage);


            if (move.move_type == MOVE_TYPE.Damage)
            {
                anim_healthbar.HookUpdate(MoveDelegateBank.DamageUpdate);
            }

 

            BattleAnimation[] arr = new BattleAnimation[]
            {
                anim_move,
                anim_healthbar
            };


            var q = new MoveQueueObj()
            {
                Animations = arr,
                Move = move,
                MoveResult = moveResult,
                User = user,
                Target = target
            };

            effect_que.Add(new List<MoveQueueObj>
            {
                q
            });

            return q;
        }

        public int GetMoveQueueCount()
        {
            return effect_que.Count;
        }
    }

    // Mostly state stuff and update and render logic
    public partial class BattleSystem
    {
        // States can be:
        // phase_select
        // phase_resolve_moves
        // phase_resolve_effects
        // (repeat from phase_select)
        public BATTLE_PHASES State;

        public enum BATTLE_PHASES
        {
            Select_Moves,
            Speed_Calc, // This is the phase where we calculate speed which shitmon get to resolve move first.
            A_Resolve_Move, // A is the shitmon that is fastest
            B_Resolve_Move, // B is the slower

            A_Resolve_Effect, // Resolves additional effects 
            B_Resolve_Effect, // Resolves additional effects 
        }

        public void SetState(BATTLE_PHASES state)
        {
            this.State = state;
        }

        public void Update(float delta)
        {
            // If animations are queded we go in here else we advance states
            if (effect_que.Count > 0)
            {
                if (effect_que[0].Count == 0)
                {
                    effect_que.RemoveAt(0);
                }
                else
                {

                    bool wait = false;

                    for (int i = 0; i < effect_que[0].Count; ++i)
                    {
                        var m = effect_que[0][i];

                        if (m.Animations != null)
                        {
                            for (int a = 0; a < m.Animations.Length; ++a)
                            {
                                var anim = m.Animations[a];

                                if (anim.anim_active)
                                {
                                    m.OnAnimStarted(); // will only trigger once inside
                                    anim.Update(delta);

                                    if (anim.anim_active)
                                    {
                                        //Console.WriteLine("Waiting for anim to complete.");
                                        wait = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (!wait)
                    {
                        for (int i = 0; i < effect_que[0].Count; ++i)
                        {
                            var m = effect_que[0][i];

                            // This is now done in queue move.
                            //if (m.Move.moveDelegate != null)
                            //    m.Move.moveDelegate.Invoke(m.User, m.Target, m.Move, out int damage);
                            //m.OnInvoked();

                            m.OnRemoved();
                            effect_que[0].Remove(m);
                        }
                    }

                }
            }
            else // State stuff, advance states
            {
                if (player.stats.GetHealthPercentage() == 0)
                {

                }

                if (enemy.stats.GetHealthPercentage() == 0)
                {

                }
            }

            Update_UI();
        }

        private void Update_UI_Level(Mon mon)
        {
            if (mon.renderData.level_i != mon.stats.level)
            {
                mon.renderData.level_str = string.Format("Lv{0}", mon.stats.level);

                mon.renderData.level_i = mon.stats.level; // to preserve string creations
            }
        }

        private void Update_UI_Healthbar(Mon mon)
        {
            mon.renderData.healthbar_rect.Width = mon.stats.GetHealthPercentage();
        }

        private void Update_UI()
        {
            Update_UI_Healthbar(player);
            Update_UI_Healthbar(enemy);

            Update_UI_Level(player);
            Update_UI_Level(enemy);
        }



        public void Render(SpriteBatch spriteBatch, MessageBox messageBox)
        {
            messageBox.Render(spriteBatch);
            messageBox.Render_Menu(spriteBatch, player);

            spriteBatch.Draw(enemy.renderData.sprite_tex, enemy.renderData.sprite_dest, enemy.renderData.sprite_rect, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(player.renderData.sprite_tex, player.renderData.sprite_dest, player.renderData.sprite_rect, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);


            // Render healthbar and color according to condition
            if (player.stats.GetHealthPercentage() < 50)
            {
                if (player.stats.GetHealthPercentage() < 25)
                    spriteBatch.Draw(player.renderData.healthbar_tex, player.renderData.healthbar_rect, Color.Red);
                else
                    spriteBatch.Draw(player.renderData.healthbar_tex, player.renderData.healthbar_rect, Color.Orange);
            }
            else
                spriteBatch.Draw(player.renderData.healthbar_tex, player.renderData.healthbar_rect, Color.Green);

            if (enemy.stats.GetHealthPercentage() < 50)
            {
                if (enemy.stats.GetHealthPercentage() < 25)
                    spriteBatch.Draw(player.renderData.healthbar_tex, enemy.renderData.healthbar_rect, Color.Red);
                else
                    spriteBatch.Draw(player.renderData.healthbar_tex, enemy.renderData.healthbar_rect, Color.Orange);
            }
            else
                spriteBatch.Draw(player.renderData.healthbar_tex, enemy.renderData.healthbar_rect, Color.Green);


            // Render mon levels
            spriteBatch.DrawString(font, enemy.renderData.level_str, enemy.renderData.level_pos, Color.Black);
            spriteBatch.DrawString(font, player.renderData.level_str, player.renderData.level_pos, Color.Black);


            // Removing anim if complete is done in Update logic and not Render logic.
            if (effect_que.Count > 0)
            {
                for (int i = 0; i < effect_que[0].Count; ++i)
                {
                    for (int a = 0; a < effect_que[0][i].Animations.Length; ++a)
                    {
                        var anim = effect_que[0][i].Animations[a];

                        if (anim.anim_active)
                        {
                            anim.Render(spriteBatch);
                            break;
                        }
                    }
                }
            }

        }
    }
}

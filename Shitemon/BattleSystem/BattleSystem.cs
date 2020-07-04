using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Shitemon.BattleSystem
{
    public class BattleSystem
    {
        Mon player, enemy;

        Rectangle r_sprite; // source rect for a sprite
        Rectangle r_player, r_enemy; // source rect for player and enemy

        
        Texture2D healthbar;
        Rectangle r_player_healthbar;
        Rectangle r_enemy_healthbar;

        public BattleSystem(ContentManager contentManager)
        {
            this.r_sprite = new Rectangle(0, 0, 64, 64);
            this.r_player = new Rectangle(50, 80, 64, 64);
            this.r_enemy = new Rectangle(200, 30, 64, 64);

            this.healthbar = contentManager.Load<Texture2D>("ui/healthbar");
            this.r_player_healthbar = new Rectangle(10, 70, healthbar.Width, healthbar.Height);
            this.r_enemy_healthbar = new Rectangle(200, 20, healthbar.Width, healthbar.Height);
        }

        public void Initialize(Mon player, Mon enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }


        // States can be:
        // phase_select
        // phase_resolve_moves
        // anim_moves
        // phase_resolve_effects
        // anim_effects
        // (repeat from phase_select)
        public string state;

        public void SetState(string state)
        {
            this.state = state;
        }

        public bool UseMove(int index)
        {
            if (player.moves[index].moveDelegate.Invoke(player, enemy, player.moves[index]))
            {
                Console.WriteLine("Player used " + player.moves[index].name + "!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetMoveAnim(Texture2D tex)
        {
            anim_tex = tex;
            anim_active = true;
            anim_t = 0;
            anim_f = 0;
        }

        public Texture2D anim_tex;
        public bool anim_active;
        public float anim_t;
        public int anim_f;

        public void Update(float delta)
        {
            if(anim_active)
            {
                anim_t += delta;

                if (anim_f == 0)
                    anim_f = 1;
                else
                    anim_f = 0;
 

                if(anim_t > 1)
                {
                    anim_active = false;
                }
            }

            r_player_healthbar.Width = player.stats.GetHealthPercentage();
            r_enemy_healthbar.Width = enemy.stats.GetHealthPercentage();
        }

        public void Render(SpriteBatch spriteBatch, MessageBox messageBox)
        {
            messageBox.Render(spriteBatch);
            messageBox.Render_Menu(spriteBatch, player);

            spriteBatch.Draw(enemy.sprite, r_enemy, r_sprite, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(player.sprite, r_player, r_sprite, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);

 
            spriteBatch.Draw(healthbar, r_player_healthbar, Color.Green);
            spriteBatch.Draw(healthbar, r_enemy_healthbar, Color.Green);

            if (anim_active)
            {
                if (anim_f == 1)
                {
                    spriteBatch.Draw(anim_tex, r_enemy, r_sprite, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
        }
    }
}

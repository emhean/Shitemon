using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shitemon.BattleSystem
{
    public static class MoveDelegateBank
    {
        // Used for move accuracy
        static Random rng = new Random(); 


        // DON'T TOUCH THIS
        static public void DamageUpdate(BattleAnimation ba, float delta)
        {
            if (ba.IsModulo_Zero())
            {
                ba.mon.stats.ReduceHealth(1);
                ba.total_damage -= 1;

                if (ba.total_damage == 0)
                {
                    ba.anim_duration = 1f;
                    ba.anim_time = 0f;
                    ba.animUpdateDelegate = null;
                }
            }

        }

        static public void MoveBlinkRender(BattleAnimation ba, SpriteBatch spriteBatch)
        {
            if(ba.IsModulo_Zero())
                spriteBatch.Draw(ba.anim_tex, ba.destRect, ba.sourceRect, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }


        static public bool Shock(Mon user, Mon target, Move move, out int damage)
        {
            // TODO: Fix
            if ((rng.Next(1, 101) > move.acc))
            {
                damage = 0;
                return false;
            }
            else
            {
                damage = (user.stats.attack + move.damage) - (target.stats.defence);
                return true;
            }
        }
    }
}

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
        static Random _rng = new Random();

        static int _rand = 0; // Randomized value
        static int _prev_rand = 0; // Previous randomized value

        static int Rand(int roof, int floor = 0)
        {
            while(_prev_rand == _rand)
                _rand = _rng.Next(floor, roof);
            
            _prev_rand = _rand;

            return _rand;
        }

        static bool RollCrit(Move move)
        {
            int i = Rand(100);
            return (i == 42);
        }

        static bool RollAccuracy(Move move)
        {
            int i = Rand(100);

            Console.WriteLine(string.Format("Move acc roll:{0} of {1}%", i, move.acc));

            // If we for example rolled 76 and move's accuracy is 75 then we miss.
            // Because it will work 75 times out of 100.
            // If 75 then it's a success, to compromise for the 0-99 thing.
            return i < move.acc; 
        }


        static EFFECTIVENESS GetEffectiveness()
        {
            return EFFECTIVENESS.Neutral;
        }


        // DON'T TOUCH THIS
        /// <summary>
        /// This method is why the health bar is "animated" 
        /// </summary>
        static public void DamageUpdate(BattleAnimation ba, float delta)
        {
            if (ba.IsModulo_Zero()) // This to make the animation a bit slower for the eyes.
            {
                if (ba.total_damage <= 0)
                {
                    ba.anim_duration = 1f;
                    ba.anim_time = 0f;
                    ba.animUpdateDelegate = null;

                   

                    return; // Fast exit
                }

                ba.mon.stats.ReduceHealth(1);
                ba.total_damage -= 1;
            }

        }

        static public void MoveBlinkRender(BattleAnimation ba, SpriteBatch spriteBatch)
        {
            if(ba.IsModulo_Zero()) // This to make the animation a bit slower for the eyes.
                spriteBatch.Draw(ba.anim_tex, ba.destRect, ba.sourceRect, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }


        static public MoveResult Shock(MoveArgs moveArgs)
        {
            bool crit = RollCrit(moveArgs.MoveUsed);
            bool hit = RollAccuracy(moveArgs.MoveUsed);

            int damage = 0;

            if(hit)
            {
                if (crit)
                    damage = (moveArgs.User.stats.attack + moveArgs.MoveUsed.damage);
                else
                    damage = (moveArgs.User.stats.attack + moveArgs.MoveUsed.damage) - (moveArgs.Target.stats.defence);
            }

            return new MoveResult(hit, crit, damage, 0);
        }
    }
}

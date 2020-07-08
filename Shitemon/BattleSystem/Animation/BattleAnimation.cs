using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shitemon.BattleSystem
{
    public class BattleAnimation
    {
        public float anim_time;
        public float anim_duration;

        // When this is set to false then animation has expired.
        public bool anim_active = true;

        public bool wait_for_completion = false;

        // Changes from one frame 0 to 1 and then next frame 1 to 0, repeat.
        int modulo = 0;

        Texture2D anim_tex;
        Rectangle destRect;
        Rectangle sourceRect;

        delegate void AnimUpdateDelegate(float delta);
        AnimUpdateDelegate animUpdateDelegate;

        delegate void AnimRenderDelegate(SpriteBatch spriteBatch);
        AnimRenderDelegate animRenderDelegate;

        /// <summary>
        /// New instance for move animation.
        /// </summary>
        public BattleAnimation(float duration, Texture2D anim_tex, Rectangle destRect, Rectangle sourceRect)
        {
            this.anim_duration = duration;
            this.anim_tex = anim_tex;
            this.destRect = destRect;
            this.sourceRect = sourceRect;
            this.animRenderDelegate += MoveBlinkRender;
        }


        Mon mon; // the shitmon receiving the damage
        int total_damage; // damage before hp reduction
        BattleSystem bs;

        /// <summary>
        /// New instance for healthbar animation.
        /// </summary>
        public BattleAnimation(BattleSystem bs, Mon mon, int total_damage)
        {
            this.bs = bs;
            this.mon = mon;

            this.total_damage = total_damage;
            this.anim_duration = 4f; 

            this.animUpdateDelegate += DamageUpdate;
        }

        public void Update(float delta)
        {
            anim_time += delta;

            //modulo = (modulo == 0) ? 1 : 0;
            if (modulo == 0)
                modulo = 1;
            else
                modulo = 0;

            if (anim_time > anim_duration)
            {
                //wait_for_completion = false;
                anim_active = false;
            }

            if (animUpdateDelegate != null)
                animUpdateDelegate.Invoke(delta);
        }

        bool IsModulo_Zero()
        {
            return (modulo == 0);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if(animRenderDelegate != null)
                animRenderDelegate.Invoke(spriteBatch);
        }


        void DamageUpdate(float delta)
        {
            if(IsModulo_Zero())
            {
                mon.stats.ReduceHealth(1);
                total_damage -= 1;

                if (total_damage == 0)
                {
                    anim_duration = 10f;
                    anim_time = 0f;

                    animUpdateDelegate = null;
                }
            }

        }


        void MoveBlinkRender(SpriteBatch spriteBatch)
        {
            if (IsModulo_Zero())
            {
                spriteBatch.Draw(anim_tex, destRect, sourceRect, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}

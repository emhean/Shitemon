using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shitemon.BattleSystem
{
    /// <summary>
    /// An animation class used during battle phases.
    /// </summary>
    public class BattleAnimation
    {
        public float anim_time;
        public float anim_duration;

        // When this is set to false then animation has expired.
        public bool anim_active = true;

        public bool Expired => !(anim_time > anim_duration);

        public void Start()
        {
            anim_active = true;
        }

        public void Stop()
        {
            anim_active = false;
        }

        // Changes from one frame 0 to 1 and then next frame 1 to 0, repeat.
        int modulo = 0;

        public bool effect_blink;

        

        public Texture2D anim_tex;
        public Rectangle destRect;
        public Rectangle sourceRect;

        public delegate void AnimUpdateDelegate(BattleAnimation ba, float delta);
        public AnimUpdateDelegate animUpdateDelegate;

        public void HookUpdate(AnimUpdateDelegate animUpdateDelegate)
        {
            this.animUpdateDelegate = animUpdateDelegate;
        }

        public delegate void AnimRenderDelegate(BattleAnimation ba, SpriteBatch spriteBatch);
        public AnimRenderDelegate animRenderDelegate;

        public void HookRender(AnimRenderDelegate animRenderDelegate)
        {
            this.animRenderDelegate = animRenderDelegate;
        }

        /// <summary>
        /// New instance for move animation.
        /// </summary>
        public BattleAnimation(float duration, Texture2D anim_tex, Rectangle destRect, Rectangle sourceRect)
        {
            this.anim_duration = duration;
            this.anim_tex = anim_tex;
            this.destRect = destRect;
            this.sourceRect = sourceRect;
        }


        public Mon mon; // the shitmon receiving the damage
        public int total_damage; // damage before hp reduction

        /// <summary>
        /// New instance for healthbar animation.
        /// </summary>
        public BattleAnimation(BattleSystem bs, Mon mon, int total_damage)
        {
            this.mon = mon;
            this.total_damage = total_damage;
            this.anim_duration = 4f; 
        }

        /// <summary>
        /// New instance for death animation.
        /// </summary>
        public BattleAnimation(BattleSystem bs, Mon mon, float anim_duration)
        {
            this.mon = mon;
            this.anim_duration = anim_duration;
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
                anim_active = false;
            }

            if (animUpdateDelegate != null)
                animUpdateDelegate.Invoke(this, delta);
        }

        public bool IsModulo_Zero()
        {
            return (modulo == 0);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (animRenderDelegate != null)
            {
                if(effect_blink && IsModulo_Zero())
                    animRenderDelegate.Invoke(this, spriteBatch);
                else
                    animRenderDelegate.Invoke(this, spriteBatch);
            }
        }

        public static void Render_DeathAnimation(BattleAnimation ba, SpriteBatch spriteBatch)
        {
            ba.mon.renderData.sprite_dest.Y += 10;
        }
    }
}

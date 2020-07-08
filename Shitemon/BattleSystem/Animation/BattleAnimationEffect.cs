using Microsoft.Xna.Framework.Graphics;

namespace Shitemon.BattleSystem
{
    // The primitive class
    public class BattleAnimationEffect
    {
        public float anim_time;
        public float anim_duration;

        // When this is set to false then animation has expired.
        public bool anim_active;

        public BattleAnimationEffect()
        {
            this.anim_duration = 1f;
        }

        public BattleAnimationEffect(float duration)
        {
            this.anim_duration = duration;
        }

        public virtual void Update(float delta)
        {
            anim_time += delta;

            if (anim_time > anim_duration)
            {
                anim_active = false;
            }
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
        }
    }
}

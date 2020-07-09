using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shitemon.BattleSystem
{
    public class MonRenderData
    {
        public Texture2D sprite_tex;
        public Rectangle sprite_rect;

        public Rectangle sprite_dest;

        public Texture2D healthbar_tex;
        public Rectangle healthbar_rect;

        // Fields for the "Lv1" or "Lv100" level shit on the screen
        public int level_i;
        public string level_str;
        public Vector2 level_pos;

        public MonRenderData(Texture2D sprite_tex, Rectangle sprite_dest, Rectangle sprite_rect, Texture2D healthbar_tex, Rectangle healthbar_rect)
        {
            this.sprite_tex = sprite_tex;
            this.sprite_dest = sprite_dest;
            this.sprite_rect = sprite_rect;
            this.healthbar_tex = healthbar_tex;
            this.healthbar_rect = healthbar_rect;

            this.level_pos = new Vector2(healthbar_rect.X - 8, healthbar_rect.Y - 16);
        }
    }
}

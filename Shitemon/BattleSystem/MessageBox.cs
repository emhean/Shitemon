using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitemon.BattleSystem
{
    public class MessageBox
    {
        Texture2D tex;
        Rectangle rect;
        SpriteFont font;

        Vector2 pos;
        Texture2D tex_cursor;

        public MessageBox(Texture2D texture, Texture2D texture_cursor, SpriteFont font)
        {
            this.tex = texture;
            this.tex_cursor = texture_cursor;

            this.font = font;
            this.rect = new Rectangle(10, 150, tex.Width, tex.Height);

            this.pos = new Vector2(18, 165);

            Init_MenuChoices();
        }

        public class MenuChoice
        {
            public string text;
            public Vector2 pos;

            public MenuChoice(string text, Vector2 pos)
            {
                this.text = text;
                this.pos = pos;
            }

            public void Render(SpriteBatch spriteBatch, SpriteFont font)
            {
                spriteBatch.DrawString(font, text, pos, Color.Black);
            }

            public void Render(SpriteBatch spriteBatch, SpriteFont font, string text)
            {
                spriteBatch.DrawString(font, text, pos, Color.Black);
            }
        }

        public string menu_current = "main";
        public int menu_index = 0;
        public MenuChoice[] menu_main;
        public MenuChoice[] menu_moves;

        void Init_MenuChoices()
        {
            menu_main = new MenuChoice[4]
            {
                new MenuChoice("MOVES", new Vector2(28, 165)),
                new MenuChoice("MONS", new Vector2(108, 165)),
                new MenuChoice("ITEMS", new Vector2(28, 200)),
                new MenuChoice("RUN", new Vector2(108, 200))
            };

            menu_moves = new MenuChoice[4]
            {
                new MenuChoice("1", new Vector2(28, 155)),
                new MenuChoice("2", new Vector2(28, 175)),
                new MenuChoice("3", new Vector2(28, 195)),
                new MenuChoice("4", new Vector2(28, 215))
            };
        }

        public void Render_Menu(SpriteBatch spriteBatch, Mon player)
        {
            if(menu_current == "main")
            {
                for (int i = 0; i < 4; ++i)
                {
                    menu_main[i].Render(spriteBatch, font);
                }

                spriteBatch.Draw(tex_cursor, menu_main[menu_index].pos - Vector2.UnitX * 10, Color.White);
            }
            else if(menu_current == "moves")
            {
                for (int i = 0; i < 4; ++i)
                {
                    menu_moves[i].Render(spriteBatch, font, player.moves[i].name);
                }

                spriteBatch.Draw(tex_cursor, menu_moves[menu_index].pos - Vector2.UnitX * 10, Color.White);
            }

        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, Color.White);
        }
    }
}

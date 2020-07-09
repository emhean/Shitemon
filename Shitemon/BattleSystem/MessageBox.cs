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



        Texture2D tex;
        Rectangle rect;
        SpriteFont font;

        Vector2 pos;
        Texture2D tex_cursor;


        class TextQueueObject
        {
            public string text;
            public int frames;

            public TextQueueObject(string text, int frames)
            {
                this.text = text;
                this.frames = frames;
            }
        }

        List<TextQueueObject> textQueue = new List<TextQueueObject>();

        public void QueueText(string text, int frames)
        {
            textQueue.Add(new TextQueueObject(text, frames));
        }

        public MessageBox(Texture2D texture, Texture2D texture_cursor, SpriteFont font)
        {
            this.tex = texture;
            this.tex_cursor = texture_cursor;

            this.font = font;
            this.rect = new Rectangle(10, 150, tex.Width, tex.Height);

            this.pos = new Vector2(18, 165);

            Init_MenuChoices();
        }

        public enum MessageboxMenus
        {
            Main,
            Moves,
            Text
        }

        public MessageboxMenus MenuCurrent = MessageboxMenus.Main;

        int menu_index = 0;
        int menu_maxIndex = 4; // to avoid selecting an index that does not have a move


        public void NextMenuIndex()
        {
            menu_index += 1;
            if (menu_index == menu_maxIndex)
                menu_index = 0;
        }
        public void PrevMenuIndex()
        {
            menu_index -= 1;
            if (menu_index == -1)
                menu_index = menu_maxIndex - 1;
        }

        public void SetMaxIndex(int value)
        {
            menu_maxIndex = value;
        }

        public int GetMenuIndex()
        {
            return menu_index;
        }

        public void ResetMenuIndex()
        {
            menu_index = 0;
        }

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
            if (MenuCurrent == MessageboxMenus.Main)
            {
                for (int i = 0; i < 4; ++i)
                {
                    menu_main[i].Render(spriteBatch, font);
                }

                spriteBatch.Draw(tex_cursor, menu_main[menu_index].pos - Vector2.UnitX * 10, Color.White);
            }
            else if (MenuCurrent == MessageboxMenus.Moves)
            {
                var moves = player.GetMoves();

                for (int i = 0; i < moves.Length; ++i)
                {
                    if (!string.IsNullOrEmpty(moves[i].name))
                        menu_moves[i].Render(spriteBatch, font, moves[i].name);
                }

                spriteBatch.Draw(tex_cursor, menu_moves[menu_index].pos - Vector2.UnitX * 10, Color.White);
            }
            else if (MenuCurrent == MessageboxMenus.Text)  // custom text, no menus
            {
                if(textQueue.Count > 0)
                {
                    spriteBatch.DrawString(font, textQueue[0].text, pos, Color.Black);

                    textQueue[0].frames -= 1;
                    if (textQueue[0].frames <= 0)
                        textQueue.RemoveAt(0);
                }
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, Color.White);
        }
    }
}

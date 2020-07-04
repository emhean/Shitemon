using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitemon.BattleSystem
{
    public class Mon
    {
        public string name;
        public Texture2D sprite;
        public Stats stats;
        public string type_1, type_2;

        public Move[] moves; // always size 4

        public Mon(string name, Texture2D sprite, Stats stats, string type_1, string type_2)
        {
            this.name = name;
            this.sprite = sprite;
            this.stats = stats;
            this.type_1 = type_1;
            this.type_2 = type_2;

            this.moves = new Move[4];
            for (int i = 0; i < 4; ++i)
                this.moves[i] = new Move();
        }
    }
}

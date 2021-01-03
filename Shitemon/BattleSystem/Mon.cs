using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitemon.BattleSystem
{
    /// <summary>
    /// The base class for a shitmon.
    /// </summary>
    public class Mon
    {
        // The name of the shitmon
        public string name;

        // The stats of the shitmon
        public Stats stats;

        // The types of the shitmon
        public TYPECHART type_1, type_2;

        public MonRenderData renderData;

        public const int MOVE_COUNT_MAX = 4;
        public const int MOVE_COUNT_MIN = 1; // will be some weird move if none are available cuz of bug or something.

        public Move[] moves; // always size 4

        public Mon(string name, MonRenderData renderData, Stats stats, TYPECHART type_1, TYPECHART type_2)
        {
            this.name = name;
            this.renderData = renderData;
            this.stats = stats;
            this.type_1 = type_1;
            this.type_2 = type_2;

            this.moves = new Move[4];
        }

        public Move[] GetMoves()
        {
            List<Move> list = new List<Move>();

            // This for loop gets the number of assigned moves up to maximum of 4.
            for (int i = 0; i < 4; ++i)
            {
                if (moves[i] != null)
                    list.Add(moves[i]);
            }

            return list.ToArray();
        }

        public int GetAssignedMovesCount()
        {
            int count = 0;

            // This for loop gets the number of assigned moves up to maximum of 4.
            for (int i = 0; i < moves.Length; ++i)
            {
                if (moves[i] != null)
                    count += 1;
            }

            return count;
        }

        public void AssignMoves(string move1, string move2, string move3, string move4)
        {
            if (!string.IsNullOrEmpty(move1))
                this.moves[0] = new Move(move1);

            if (!string.IsNullOrEmpty(move2))
                this.moves[1] = new Move(move2);

            if (!string.IsNullOrEmpty(move3))
                this.moves[2] = new Move(move3);

            if (!string.IsNullOrEmpty(move4))
                this.moves[3] = new Move(move4);
        }
    }
}

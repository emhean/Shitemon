namespace Shitemon.BattleSystem
{
    public class Move
    {
        public delegate bool MoveDelegate(Mon user, Mon target, Move move_used);

        public string name;
        public int pp;
        public int damage;
        public string type;

        public MoveDelegate moveDelegate;

        public Move()
        {
            this.name = "Shock";
            this.pp = 40;
            this.damage = 10;
            this.type = "Electric";
            this.moveDelegate = MoveDelegateBank.Shock;
        }
    }
}

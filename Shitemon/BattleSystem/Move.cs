namespace Shitemon.BattleSystem
{
    public class Move
    {
        public delegate bool MoveDelegate(Mon user, Mon target, Move move_used);

        public string name;
        public int pp;
        public int damage;
        public int acc;
        public string type;

        public MoveDelegate moveDelegate;

        public Move(string name)
        {
            this.name = name;

            if(name.Equals("Shock"))
            {
                this.pp = 40;
                this.damage = 10;
                this.acc = 100;
                this.type = "Electric";
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Electric Bolt"))
            {
                this.pp = 30;
                this.damage = 20;
                this.acc = 100;
                this.type = "Electric";
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Thunder"))
            {
                this.pp = 20;
                this.damage = 25;
                this.acc = 95;
                this.type = "Electric";
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Lightning Bolt"))
            {
                this.pp = 15;
                this.damage = 30;
                this.acc = 90;
                this.type = "Electric";
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Lightning"))
            {
                this.pp = 10;
                this.damage = 40;
                this.acc = 75;
                this.type = "Electric";
                this.moveDelegate = MoveDelegateBank.Shock;
            }

        }
    }
}

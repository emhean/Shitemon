using System;

namespace Shitemon.BattleSystem
{
    /// <summary>
    /// The logic of a move that a shitmon has.
    /// </summary>
    public class Move
    {
        // The function delegate that returns a result of the move.
        public delegate MoveResult MoveDelegate(MoveArgs moveArgs); // MoveArgs is event args for move

        public string name;
        public string asset_name; // asset name such as sound effects? or texture?
        public int pp; // power points, determines use
        public int damage; 
        public int acc; //accuraccy

        public TYPECHART type;
        public MOVE_TYPE move_type = MOVE_TYPE.NaN;

        public MoveDelegate moveDelegate;


        public Move(string name)
        {
            this.name = name;

            if (name.Equals("Dick Slam"))
            {
                this.pp = 40;
                this.damage = 77;
                this.acc = 100;
                this.type = TYPECHART.Robotic;
                this.asset_name = "shock";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;
            }

            if (name.Equals("Shock"))
            {
                this.pp = 40;
                this.damage = 10;
                this.acc = 75;
                this.type = TYPECHART.Robotic;
                this.asset_name = "shock";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;

            }
            else if (name.Equals("Discharge"))
            {
                this.pp = 30;
                this.damage = 20;
                this.acc = 100;
                this.type = TYPECHART.Robotic;
                this.asset_name = "discharge";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Thunder"))
            {
                this.pp = 20;
                this.damage = 25;
                this.acc = 95;
                this.type = TYPECHART.Robotic;
                this.asset_name = "thunder";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Lightning Bolt"))
            {
                this.pp = 15;
                this.damage = 30;
                this.acc = 90;
                this.type = TYPECHART.Robotic;
                this.asset_name = "lightning_bolt";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;
            }
            else if (name.Equals("Lightning"))
            {
                this.pp = 10;
                this.damage = 60;
                this.acc = 50;
                this.type = TYPECHART.Robotic;
                this.asset_name = "lightning";

                this.move_type = MOVE_TYPE.Damage;
                this.moveDelegate = MoveDelegateBank.Shock;
            }

        }
    }
}

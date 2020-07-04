using System;

namespace Shitemon.BattleSystem
{
    public class Stats
    {
        public int health, health_max;
        public int attack, attack_max;
        public int defence, defence_max;
        public int speed, speed_max;


        public Stats(int health, int defence, int attack, int speed)
        {
            this.health = health;
            this.health_max = health;
            this.attack = attack;
            this.attack_max = attack;
            this.defence = defence;
            this.defence_max = defence;
            this.speed = speed;
            this.speed_max = speed;
        }

        public int GetHealthPercentage()
        {
            return GetStatPercentage(health, health_max);
        }

        public int GetStatPercentage(int value, int max_value)
        {
            // Division by 0 preventation.
            if (value == 0 || max_value == 0)
                return 0;

            // DO NOT TOUCH !!!
            // We multiply by 100 and then divide by 100 to avoid using the primitive type decimal
            // and afterward casting it to an integer.
            // Doing it the normnal way: (health) / (health_max)) * 100;
            //      results in 0 if not 100, because that's how integer works.
            int p = (value * 100 / max_value * 100) / 100;
            return (p > 0 ? p : 0); // Return p if above 0 else return 0.
        }
    }
}

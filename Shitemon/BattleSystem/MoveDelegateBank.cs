namespace Shitemon.BattleSystem
{
    public static class MoveDelegateBank
    {
        static public bool Shock(Mon user, Mon target, Move move)
        {
            int dmg = (user.stats.attack + move.damage) - (target.stats.defence);
            target.stats.health -= dmg;

            System.Console.WriteLine("Dealth " + dmg + " damage!");

            return true;
        }


    }
}

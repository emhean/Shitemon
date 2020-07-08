namespace Shitemon.BattleSystem
{
    /// <summary>
    /// Utilities.
    /// </summary>
    public static class Utils
    {
        static public float GetTypechartModifier(Move sender, Mon receiver, out string message)
        {
            TYPECHART s = sender.type;

            TYPECHART r1 = receiver.type_1;
            TYPECHART r2 = receiver.type_2;

            float value = 1f; // 1 is neutral

            value = CalculateTable(s, r1);
            if (r2 != TYPECHART.None)
                value -= CalculateTable(s, r2);

            if (value > 1.0f)
            {
                message = "It's super effective!";
                return 2.0f;
            }
            else if (value < 1.0f)
            {
                message = "It's not very effective...";
                return 0.5f;
            }
            else
            {
                message = "It kinda works!";
                return 1.0f;
            }
        }

        static public float CalculateTable(TYPECHART attacking, TYPECHART defending)
        {
            if (attacking == TYPECHART.Fire)
            {
                if (defending == TYPECHART.Water) // not effective
                    return 0.5f;
                else if (defending == TYPECHART.Grass) // effective
                    return 2f;
            }
            else if (attacking == TYPECHART.Grass)
            {
                if (defending == TYPECHART.Fire)//not effective
                    return 0.5f;
                else if (defending == TYPECHART.Water)
                    return 2f;
            }
            else if (attacking == TYPECHART.Water)
            {
                if (defending == TYPECHART.Grass)//not effective
                    return 0.5f;
                else if (defending == TYPECHART.Fire)
                    return 2f;
            }
            else if (attacking == TYPECHART.Electric)
            {
                if (defending == TYPECHART.Ground)//not effective
                    return 0.5f;
                else if (defending == TYPECHART.Water)
                    return 2f;
            }
            else if (attacking == TYPECHART.Ground)
            {
                if (defending == TYPECHART.Grass)//not effective
                    return 0.5f;
                else if (defending == TYPECHART.Electric)
                    return 2f;
            }

            return 1f;
        }
    }
}

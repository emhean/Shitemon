namespace Shitemon.BattleSystem
{
    public enum EFFECTIVENESS
    {
        NotVeryEffective = 0,
        Neutral = 1,
        SuperEffective = 2
    }

    public enum TYPECHART
    {
        NaN,
        Plant,
        Plastic,
        Undead,
        Light,
        Explosive,
        Robotic,
    }

    public enum MOVE_TYPE
    {
        NaN,
        Damage,
        Burn,
        OHKO,
        Buff_Atk,
        Buff_Def,
        Buff_Spd,
        Heal
    }
}

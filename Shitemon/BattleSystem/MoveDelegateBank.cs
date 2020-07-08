using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Shitemon.BattleSystem
{
    public static class MoveDelegateBank
    {
        //static SoundEffectInstance _sfx;
        //static ContentManager _cm;

        //internal static void SetContentManager(ContentManager content)
        //{
        //    _cm = content;
        //}

        //static void PlaySFX(string name)
        //{
        //    _sfx = _cm.Load<SoundEffect>(name).CreateInstance();
        //    _sfx.Play();
        //}

        static public bool Shock(Mon user, Mon target, Move move, out int damage)
        {
            damage = (user.stats.attack + move.damage) - (target.stats.defence);

            Console.WriteLine("Dealt " + damage + "!");

            return true;
        }
    }
}

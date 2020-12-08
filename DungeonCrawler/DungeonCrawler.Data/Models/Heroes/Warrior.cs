using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Warrior : Hero
    {
        public Warrior() { }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"\tAbility\t\t\t Ravage\n\t\t\t\t Sacrifice 15% of max HP for double damage for one round\n";
        }
    }
}

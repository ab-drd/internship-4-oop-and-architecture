using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Mage : Hero
    {
        public Mage() { }

        public int Mana { get; set; }

        public bool Resurrection { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"\tMana\t\t\t {Mana}\n" +
                $"\tAbility\t\t\t Resurrection (cheat death once)";
        }

    }
}

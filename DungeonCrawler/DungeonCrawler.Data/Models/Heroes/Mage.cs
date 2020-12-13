using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Mage : Hero
    {
        public Mage()
        {

        }

        public Mage(string heroName) : base(heroName)
        {
            Health = 50;
            CurrentHealth = 50;
            Damage = 50;

            Mana = 100;
            CurrentMana = 100;

            ManaAttackCost = 25;
            ManaHealCost = 50;
            ManaHealAmount = 25;

            Resurrection = true;
            
        }

        public int Mana { get; set; }
        public int CurrentMana { get; set; }

        public int ManaAttackCost { get; set; }
        public int ManaHealCost { get; set; }
        public int ManaHealAmount { get; set; }

        public bool Resurrection { get; set; }

        public override string ToString()
        {
            if(Resurrection)
            {
                return $"{base.ToString()}" +
                $"\tMana\t\t\t {CurrentMana}/{Mana}\n" +
                $"\tMana Attack Cost\t {ManaAttackCost}\n" +
                $"\tMana Heal \t\t {ManaHealCost} Mana to heal {ManaHealAmount} HP\n\n" +
                $"\tAbility\t\t\t Resurrection (cheat death once)\n" +
                $"\t\t\t\t UNUSED";
            }

            else
            {
                return $"{base.ToString()}" +
                $"\tMana\t\t\t {CurrentMana}/{Mana}\n" +
                $"\tMana Attack Cost\t {ManaAttackCost}\n" +
                $"\tMana Heal \t\t {ManaHealCost} Mana to heal {ManaHealAmount} HP\n\n" +
                $"\tAbility\t\t\t Resurrection (cheat death once)\n" +
                $"\t\t\t\t USED";
            }
            
        }

    }
}

using DungeonCrawler.Data.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Helpers
{
    public static class PrintHelpers
    {
        public static void PrintClass(int yourClass)
        {
            switch(yourClass)
            {
                case 1:
                    Console.WriteLine("Class\t\t Warrior");
                    break;

                case 2:
                    Console.WriteLine("Class\t\t Mage");
                    break;

                case 3:
                    Console.WriteLine("Class\t\t Ranger");
                    break;

            }
        }

        public static void PrintHealthAndDamage(Hero hero)
        {
            Console.WriteLine($"Health\t\t {hero.Health}\n" +
                $"Damage\t\t {hero.Damage}");
        }
    }
}

using DungeonCrawler.Data;
using DungeonCrawler.Data.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Helpers
{
    public static class PrintHelpers
    {
        public static void StatPrint(Hero hero, int monsterIndex)
        {
            ColorPrints.ColorGreen(" > THE HERO\n");
            Console.WriteLine(hero);

            ColorPrints.ColorRed("\n > THE MONSTER\n");
            Console.WriteLine(DataStore.AllMonsters[monsterIndex]);
        }



        public static void WinScreen()
        {
            ColorPrints.ColorGreen("\n\n\tTHE HERO HAS WON!\n\n");

            Console.WriteLine("Congratulations!\n\n" +
                            "Against all odds, you came out on top.\n" +
                            "Go treat yourself :)\n\n" +
                            "Thanks for playing!");

            Console.WriteLine(" Press any key to exit the game");
            Console.ReadKey();

            Console.Clear();
        }



        public static bool DeathScreen(int monstersSlain)
        {
            Console.Clear();

            ColorPrints.ColorRed("\n\n\tYOU DIED.\n\n");

            Console.WriteLine("\n Your hero's HP dropped to 0.\n" +
                $" Your run ended with {monstersSlain} monster(s) slain.\n\n" +
                $" Try again?\n" +
                $" 1\t Try again!\n" +
                $" 2\t I'm done for today\n");

            var gameChoice = Input.IntInputAndCheck(1, 2);

            switch (gameChoice)
            {
                case 1:
                    return true;
                case 2:
                    return false;
            }

            return false;
        }
    }
}

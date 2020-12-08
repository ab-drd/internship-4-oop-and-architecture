using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Helpers
{
    public static class ColorPrints
    {
        public static void ColorCyan(string printThis)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(printThis);
            
            Console.ResetColor();

        }

        public static void ColorRed(string printThis)
        {

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(printThis);

            Console.ResetColor();

        }

        public static void ColorYellow(string printThis)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(printThis);

            Console.ResetColor();

        }

        public static void ColorGreen(string printThis)
        {

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(printThis);

            Console.ResetColor();

        }
    }
}

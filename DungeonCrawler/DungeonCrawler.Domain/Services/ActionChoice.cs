using DungeonCrawler.Data.Enums;
using DungeonCrawler.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Services
{
    public static class ActionChoice
    {
        
        public static RockPaperScissors ActionChoices(bool user)
        {
            var chooseAction = 0;

            if (user)
            {
                Console.WriteLine(" Choose action:\n" +
                " 1\t Forward Attack (Rock)\n" +
                " 2\t Side Attack (Paper)\n" +
                " 3\t Counterattack (Scissors)\n");

                chooseAction = IntegerInput.IntInputAndCheck(1, 3);
            }
            
            else
            {
                var r = new Random();
                chooseAction = r.Next(1, 3);
            }
            
            var choice = RockPaperScissors.Paper;

            switch(chooseAction)
            {
                case 1:
                    choice = RockPaperScissors.Rock;
                    break;

                case 2:
                    choice = RockPaperScissors.Paper;
                    break;

                case 3:
                    choice = RockPaperScissors.Scissors;
                    break;
                
            }

            return choice;
        }

        public static int RoundOutcome(RockPaperScissors heroChoice, RockPaperScissors monsterChoice)
        {
            if(heroChoice == monsterChoice)
            {
                return 1;
            }
            else if (heroChoice == RockPaperScissors.Paper && monsterChoice == RockPaperScissors.Rock
                || heroChoice == RockPaperScissors.Scissors && monsterChoice == RockPaperScissors.Paper
                || heroChoice == RockPaperScissors.Rock && monsterChoice == RockPaperScissors.Scissors)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}

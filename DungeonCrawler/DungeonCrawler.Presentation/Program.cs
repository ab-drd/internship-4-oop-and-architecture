using DungeonCrawler.Data;
using DungeonCrawler.Data.Enums;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Domain.Helpers;
using DungeonCrawler.Domain.Services;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopCondition = false;

            while(!stopCondition)
            {
                Console.Clear();

                ColorPrints.ColorRed("\n >>>>>>>> DUNGEON CRAWLER <<<<<<<<\n\n\n");

                ColorPrints.ColorCyan(" MAIN MENU\n");

                Console.WriteLine(" 1\t PLAY\n" +
                    " 2\t EXIT\n");

                var playOrExitChoice = IntegerInput.IntInputAndCheck(1, 2);

                switch(playOrExitChoice)
                {
                    case 1:
                        //var instanceHero = CharacterCreation.CreateCharacter();

                        //testHero, for test purposes
                        var newHero = new Warrior
                        {
                            HeroName = "TestHero v1",
                            HeroClass = HeroClass.Warrior,
                            Health = 100,
                            CurrentHealth = 100,
                            Damage = 25,
                            Experience = 0,
                            Level = 1

                        };

                        var instanceHero = newHero;

                        if (instanceHero == null)
                        {
                            stopCondition = true;
                        }

                        else
                        {
                            for(var i = 0; i < 10; i++)
                            {
                                RandomEnemy.CreateRandomEnemy(i);
                            }

                            Console.Clear();

                            stopCondition = BattleSimulator.BattleSimulation(instanceHero);
                        }

                        break;

                    case 2:
                        stopCondition = true;
                        break;

                }
            }
        }
    }
}

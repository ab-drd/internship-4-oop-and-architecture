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
            var isPersonPlaying = true;

            while(isPersonPlaying)
            {
                Console.Clear();

                ColorPrints.ColorRed("\n >>>>>>>> DUNGEON CRAWLER <<<<<<<<\n\n");

                ColorPrints.ColorCyan(" MAIN MENU\n");

                Console.WriteLine(" 1\t PLAY\n" +
                    " 2\t EXIT\n");

                var playOrExitChoice = Input.IntInputAndCheck(1, 2);

                switch(playOrExitChoice)
                {
                    case 1:
                        //var instanceHero = CharacterCreation.CreateCharacter();

                        // test Heroes, for test purposes

                        /*var newHero = new Warrior
                        {
                            HeroName = "TestHero Warrior v1",
                            HeroClass = HeroClass.Warrior,

                            Health = 100,
                            CurrentHealth = 100,
                            
                            Experience = 0,
                            Level = 1,

                            Damage = 20

                        };*/

                        var newHero = new Mage
                        {
                            HeroName = "TestHero Mage v1",
                            HeroClass = HeroClass.Warrior,

                            Health = 50,
                            CurrentHealth = 50,
                            
                            Experience = 0,
                            Level = 1,

                            Damage = 50,
                            Mana = 100,
                            CurrentMana = 100,

                            ManaAttackCost = 25,
                            ManaHealCost = 50,
                            ManaHealAmount = 25,

                            Resurrection = true

                        };

                        var instanceHero = newHero;
                        

                        if (instanceHero == null)
                        {
                            isPersonPlaying = false;
                        }

                        else
                        {
                            DataStore.AllMonsters.Clear();

                            for(var i = 0; i < 10; i++)
                            {
                                RandomEnemy.CreateRandomEnemy(i);
                            }

                            Console.Clear();

                            isPersonPlaying = BattleSimulator.GeneralBattleSimulation(instanceHero);
                        }

                        break;

                    case 2:
                        isPersonPlaying = false;
                        break;

                }
            }
        }
    }
}

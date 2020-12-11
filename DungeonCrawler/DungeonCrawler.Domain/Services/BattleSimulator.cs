using DungeonCrawler.Data;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Data.Models.Monsters;
using DungeonCrawler.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Services
{
    public static class BattleSimulator
    {
        public static bool GeneralBattleSimulation(Hero instanceHero)
        {
            var returnBool = false;

            var localDamage = instanceHero.Damage;

            var r = new Random();

            var monstersDefeated = 0;

            for (var i = 0; i < DataStore.AllMonsters.Count; i++)
            {
                var roundNumber = 1;

                while (instanceHero.CurrentHealth > 0 && DataStore.AllMonsters[i].CurrentHealth > 0)
                {
                    ColorPrints.ColorYellow($"\n   >>> ENEMY {i + 1} / {DataStore.AllMonsters.Count} || ROUND {roundNumber++} <<<\n");

                    StatPrint(instanceHero, i);

                    var heroChoice = ActionChoice.ActionChoices(true);

                    var monsterChoice = ActionChoice.ActionChoices(false);

                    Console.WriteLine();

                    if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 1)
                    {
                        Console.WriteLine($" {instanceHero.HeroName} and the monster dodged eachother's attacks!\n");
                    }

                    else if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 2)
                    {
                        Console.WriteLine(" The hero prevailed!");

                        if (instanceHero is Warrior warrior)
                        {
                            localDamage *= ActionChoice.WarriorRavage(warrior);
                        }

                        if (instanceHero is Mage mage)
                        {
                            localDamage = ActionChoice.MageAttackChoice(mage);
                        }

                        if (instanceHero is Ranger ranger)
                        {

                        }
                        
                        DataStore.AllMonsters[i].CurrentHealth -= localDamage;

                        Console.WriteLine($" The Monster's HP has been reduced by {localDamage}!\n");

                        localDamage = instanceHero.Damage;
                    }

                    else
                    {
                        var healthDrop = RandomEnemy.MonsterAction(instanceHero, DataStore.AllMonsters[i]);
                        instanceHero.CurrentHealth = instanceHero.CurrentHealth - healthDrop;
                    }

                    Console.WriteLine(" Press any key to continue");
                    Console.ReadKey();

                    Console.Clear();
                }

                if ((instanceHero is Mage mageInstance) && mageInstance.CurrentHealth < 1 &&mageInstance.Resurrection)
                {
                    var mageContinues = ActionChoice.MageResurrection(mageInstance);

                    if (!mageContinues)
                    {
                        returnBool = DeathScreen(i);
                        
                        break;
                    }
                    else
                    {
                        MonsterDefeated(instanceHero, i);
                        monstersDefeated++;
                    }
                }

                else if (DataStore.AllMonsters[i].CurrentHealth < 1)
                {
                    MonsterDefeated(instanceHero, i);
                    monstersDefeated++;
                }

                else
                {
                    returnBool = DeathScreen(i);
                    break;
                }

                Console.WriteLine("\n Press any key to continue");
                Console.ReadKey();

                Console.Clear();

            }

            if(monstersDefeated == DataStore.AllMonsters.Count)
            {
                WinScreen();
            }

            return returnBool;
        }

        public static bool DeathScreen(int i)
        {
            Console.Clear();

            ColorPrints.ColorRed("\n\n\tYOU DIED.\n\n");

            Console.WriteLine("\n Your hero's HP dropped to 0.\n" +
                $" Your run ended with {i} monster(s) slain.\n\n" +
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



        public static void MonsterDefeated(Hero hero, int monsterIndex)
        {

            Console.WriteLine("\n You've defeated the monster!\n");

            if (DataStore.AllMonsters[monsterIndex] is Witch witch)
            {
                Console.WriteLine(" The Witch chanted a summoning spell just as she took her final breath.\n" +
                                    " Two additional, even stronger monsters now stand in your way!\n");

                RandomEnemy.CreateRandomEnemy(10);
                RandomEnemy.CreateRandomEnemy(20);
            }

            if (hero.CurrentHealth + hero.Health / 4 > hero.Health)
            {
                hero.CurrentHealth = hero.Health;
            }

            else
            {
                hero.CurrentHealth += hero.Health / 4;
            }

            Console.WriteLine(" Your Health has been restored by 25%\n" +
                            $" Health\t\t\t {hero.CurrentHealth}/{hero.Health}");

            if(hero is Mage mage)
            {
                mage.CurrentMana = mage.Mana;
                Console.WriteLine("\n Your Mana has been fully restored\n" +
                                 $" Mana\t\t\t {mage.CurrentMana}/{mage.Mana}");
            }

            if (hero.CurrentHealth != hero.Health)
            {
                Console.WriteLine("\n Would you like to forfeit this battle's experience to regain all of your HP?\n" +
                                " 1\t Yes, heal me!\n" +
                                " 2\t No, I can go on without resorting to such measures.\n");

                var healChoice = Input.IntInputAndCheck(1, 2);

                if (healChoice == 1)
                {
                    hero.CurrentHealth = hero.Health;
                    DataStore.AllMonsters[monsterIndex].Experience = 0;
                    Console.WriteLine("\n You're now at full HP!\n");
                }
            }

            hero.Experience += DataStore.AllMonsters[monsterIndex].Experience;

            LevelUpHelper.LevelUp(hero);

        }



        public static void StatPrint(Hero hero, int monsterIndex)
        {
            ColorPrints.ColorGreen(" > THE HERO\n");
            Console.WriteLine(hero);

            ColorPrints.ColorRed(" > THE MONSTER\n");
            Console.WriteLine(DataStore.AllMonsters[monsterIndex]);
        }
    }
}

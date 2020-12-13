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
                var isStunned = false;

                while (instanceHero.CurrentHealth > 0 && DataStore.AllMonsters[i].CurrentHealth > 0)
                {
                    ColorPrints.ColorYellow($"\n   >>> ENEMY {i + 1} / {DataStore.AllMonsters.Count} || ROUND {roundNumber++} <<<\n");

                    PrintHelpers.StatPrint(instanceHero, i);

                    var heroChoice = ActionChoice.ActionChoices(true);

                    var monsterChoice = ActionChoice.ActionChoices(false);

                    Console.WriteLine();

                    if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 1)
                    {
                        Console.WriteLine($" {instanceHero.HeroName} and the {DataStore.AllMonsters[i].MonsterType} " +
                                           "dodged eachother's attacks!\n");
                    }

                    else if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 2)
                    {
                        Console.WriteLine(" The hero prevailed!\n");

                        if (instanceHero is Warrior warrior)
                        {
                            localDamage *= ActionChoice.WarriorAttack(warrior);
                        }

                        if (instanceHero is Mage mage)
                        {
                            localDamage = ActionChoice.MageAttackChoice(mage);
                        }

                        if (instanceHero is Ranger ranger)
                        {
                            localDamage = ActionChoice.RangerAttack(ranger);
                            if(ranger.StunSuccess)
                            {
                                isStunned = true;
                            }
                        }
                        
                        DataStore.AllMonsters[i].CurrentHealth -= localDamage;

                        Console.WriteLine($" The {DataStore.AllMonsters[i].MonsterType}'s HP " +
                                          $"has been reduced by {localDamage}!\n");

                        localDamage = instanceHero.Damage;
                    }

                    else
                    {
                        if(!isStunned)
                        {
                            var healthDrop = RandomEnemy.MonsterAction(instanceHero, DataStore.AllMonsters[i]);
                            instanceHero.CurrentHealth -= healthDrop;
                        }

                        else
                        {
                            Console.WriteLine($" The {DataStore.AllMonsters[i].MonsterType} could not move!\n");

                            if (instanceHero is Ranger rangerInstance)
                            {
                                rangerInstance.StunSuccess = false;
                            }
                        }
                    }

                    Console.WriteLine(" Press any key to continue");
                    Console.ReadKey();

                    Console.Clear();
                }

                if ((instanceHero is Mage mageInstance) && mageInstance.CurrentHealth < 1 && mageInstance.Resurrection)
                {
                    var mageContinues = ActionChoice.MageResurrection(mageInstance);

                    if (!mageContinues)
                    {
                        returnBool = PrintHelpers.DeathScreen(i);
                        break;
                    }

                    else
                    {
                        monstersDefeated++;
                    }
                }

                else if (DataStore.AllMonsters[i].CurrentHealth < 1)
                {
                    PostBattle(instanceHero, i);
                    monstersDefeated++;
                }

                else
                {
                    returnBool = PrintHelpers.DeathScreen(i);
                    break;
                }

                Console.WriteLine("\n Press any key to continue");
                Console.ReadKey();

                Console.Clear();

            }

            if(monstersDefeated == DataStore.AllMonsters.Count)
            {
                PrintHelpers.WinScreen();
            }

            return returnBool;
        }
        


        public static void PostBattle(Hero hero, int monsterIndex)
        {
            ColorPrints.ColorMagenta($"\n   >>> THE MONSTER HAS BEEN DEFEATED <<<\n");
            Console.WriteLine($" You've defeated the {DataStore.AllMonsters[monsterIndex].MonsterType}!\n");

            if (DataStore.AllMonsters[monsterIndex] is Witch witch)
            {
                RandomEnemy.WitchEnemySummon();
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
                Console.WriteLine("\n Would you like to forfeit half of this battle's experience to regain all of your HP?\n" +
                                " 1\t Yes, heal me!\n" +
                                " 2\t No, I can go on without it!\n");

                var healChoice = Input.IntInputAndCheck(1, 2);

                if (healChoice == 1)
                {
                    hero.CurrentHealth = hero.Health;
                    DataStore.AllMonsters[monsterIndex].Experience = DataStore.AllMonsters[monsterIndex].Experience / 2;
                    Console.WriteLine("\n You're now at full HP!\n");
                }
            }

            hero.Experience += DataStore.AllMonsters[monsterIndex].Experience;

            LevelUpHelper.LevelUp(hero);

        }



        
    }
}

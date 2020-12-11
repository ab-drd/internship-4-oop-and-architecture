using DungeonCrawler.Data.Enums;
using DungeonCrawler.Data.Models.Heroes;
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

                chooseAction = Input.IntInputAndCheck(1, 3);
            }
            
            else
            {
                var r = new Random();
                chooseAction = StaticRandom.GetRandom(1, 3);
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



        public static int WarriorRavage(Warrior warrior)
        {
            Console.WriteLine("\n You're a warrior, a force of fury and blood!");
            
            Console.WriteLine(" Would you like to sacrifice 15% of your max HP to double your damage this round?\n" +
                    " 1\t Yes\n" +
                    " 2\t No\n");

            var warriorChoice = Input.IntInputAndCheck(1, 2);

            switch (warriorChoice)
            {
                case 1:
                    if (warrior.CurrentHealth <= warrior.Health * 3 / 20)
                    {
                        Console.WriteLine(" You can't off yourself like that!\n" +
                            " (activating Ravage would kill you, so you've been stopped)");

                        return 1;
                    }

                    else
                    {
                        warrior.CurrentHealth -= warrior.Health * 3 / 20;
                        Console.WriteLine($" Your stats are now:\n" +
                            $" Health\t\t {warrior.CurrentHealth}\n" +
                            $" Damage\t\t {warrior.Damage * 2}");

                        return 2;
                    }

                case 2:
                    Console.WriteLine("\n You chose not to sacrifice your HP for a temporary boost.\n");
                    return 1;

                default:
                    return 1;
            }
        }



        public static bool MageResurrection(Mage mage)
        {
            Console.WriteLine(" The gods smile upon you and decide to let you resurrect once and continue fighting.\n" +
                    " Will you take this offer?\n" +
                    " 1\t Yes!\n" +
                    " 2\t No, I'm done with this life.\n");

            var mageRessurectChoice = Input.IntInputAndCheck(1, 2);

            switch(mageRessurectChoice)
            {
                case 1:
                    mage.Resurrection = false;
                    Console.WriteLine("\n Your lifeless body springs back into life!\n" +
                                      " The energy surging around you pushes away the " +
                                      "monster, and you run past it! Lucky!\n");

                    mage.CurrentHealth = mage.Health;
                    
                    return true;

                default:
                    return false;
            }
        }



        public static int MageAttackChoice(Mage mage)
        {
            var damageDealt = 0;

            if(mage.CurrentMana < mage.ManaAttackCost)
            {
                Console.WriteLine($" You don't not have enough Mana to perform an attack!\n" +
                    $" This round will be used to replenish your mana.\n" +
                    $" Your mana is now full!\n");

                mage.CurrentMana = mage.Mana;
            }

            else
            {
                var doNormalAttack = true;

                Console.WriteLine($"\n Mana Attack Cost\t{mage.ManaAttackCost}\n" +
                                  $" Mana Heal Cost\t\t{mage.ManaHealCost}");

                if(mage.CurrentHealth < mage.Health && mage.CurrentMana >= mage.ManaHealCost)
                {
                    Console.WriteLine($"\n You have enough Mana to perform a healing spell, which would heal {mage.ManaHealAmount} HP.\n\n" +
                                      $" Will you heal?\n" +
                                      $" 1\t Heal me!\n" +
                                      $" 2\t No, I want to attack!\n");

                    var mageHealChoice = Input.IntInputAndCheck(1, 2);

                    switch(mageHealChoice)
                    {
                        case 1:
                            Console.WriteLine("\n Heavenly light bathes your skin and you start feeling better!\n");

                            mage.CurrentHealth += mage.ManaHealAmount;

                            if(mage.CurrentHealth > mage.Health)
                            {
                                mage.CurrentHealth = mage.Health;
                            }

                            mage.CurrentMana -= mage.ManaHealCost;

                            Console.WriteLine($" Your Health is now {mage.CurrentHealth}/{mage.Health}");

                            doNormalAttack = false;

                            break;

                        case 2:
                            break;

                    }
                }

                if(doNormalAttack)
                {
                    Console.WriteLine("\n You concentrate magic into the palm of " +
                                     $" your hand and launch it into your enemy!\n");

                    damageDealt = mage.Damage;
                    mage.CurrentMana -= mage.ManaAttackCost;

                }
            }
            
            return damageDealt;
        }
    }
}

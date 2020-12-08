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
        public static bool BattleSimulation(Hero instanceHero)
        {
            if(instanceHero is Warrior warrior)
            {
                BattleSimulatorWarrior(warrior);
            }

            else if(instanceHero is Mage mage)
            {
                BattleSimulatorMage(mage);
            }

            else if(instanceHero is Ranger ranger)
            {
                BattleSimulatorRanger(ranger);
            }

            return false;
        }

        

        public static bool BattleSimulatorWarrior(Warrior warrior)
        {
            var localDamage = warrior.Damage;

            var numberOfEnemies = 10;

            var r = new Random();

            for (var i = 0; i < numberOfEnemies; i++)
            {
                var round = 1;
                while (warrior.CurrentHealth > 0 && DataStore.AllMonsters[i].CurrentHealth > 0)
                {
                    ColorPrints.ColorYellow($"\n   >>> ENEMY {i + 1} / {numberOfEnemies} || ROUND {round++} <<<\n");

                    StatPrint(warrior, i);

                    var heroChoice = ActionChoice.ActionChoices(true);

                    var monsterChoice = ActionChoice.ActionChoices(false);

                    Console.WriteLine();

                    if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 1)
                    {
                        Console.WriteLine($" {warrior.HeroName} and the monster dodged eachother's attacks!");
                    }

                    else if (ActionChoice.RoundOutcome(heroChoice, monsterChoice) == 2)
                    {
                        Console.WriteLine(" The hero prevailed!");

                        if (i == 0)
                        {
                            Console.WriteLine("\n You're a warrior, a force of fury and blood!");
                        }

                        Console.WriteLine(" Would you like to sacrifice 15% of your max HP to double your damage this round?\n" +
                                " 1\t Yes\n" +
                                " 2\t No\n");

                        var warriorChoice = IntegerInput.IntInputAndCheck(1, 2);
                        switch (warriorChoice)
                        {
                            case 1:
                                if (warrior.CurrentHealth <= warrior.Health * 3 / 20)
                                {
                                    Console.WriteLine(" You can't off yourself like that!\n" +
                                        " (activating Ravage would kill you, so you've been stopped)");
                                }
                                else
                                {
                                    localDamage = localDamage * 2;
                                    warrior.CurrentHealth -= warrior.Health * 3 / 20;
                                    Console.WriteLine($" Your stats are now:\n" +
                                        $" Health\t\t {warrior.CurrentHealth}\n" +
                                        $" Damage\t\t {localDamage}");
                                }

                                break;

                            case 2:
                                Console.WriteLine(" You chose not to sacrifice your HP for a temporary boost.\n");
                                break;
                        }

                        var healthDrop = r.Next(localDamage - 2, localDamage + 2);
                        DataStore.AllMonsters[i].CurrentHealth -= healthDrop;

                        Console.WriteLine($" The Monster's HP has been reduced by {healthDrop}!\n");

                        localDamage = warrior.Damage;

                    }

                    else
                    {
                        var healthDrop = RandomEnemy.EnemyAction(warrior, DataStore.AllMonsters[i]);
                        warrior.CurrentHealth = warrior.CurrentHealth - healthDrop;
                    }

                    Console.WriteLine(" Press any key to continue");
                    Console.ReadKey();

                    Console.Clear();

                }

                if (warrior.CurrentHealth < 1)
                {
                    DeathScreen(i);
                    break;
                }
                else if (DataStore.AllMonsters[i].CurrentHealth < 1)
                {
                    numberOfEnemies += MonsterDefeated(warrior, i);
                }

                Console.WriteLine("\n Press any key to continue");
                Console.ReadKey();

                Console.Clear();

            }

            return false;
        }

        public static bool BattleSimulatorMage(Mage mage)
        {
            var localHealth = mage.Health;
            var localDamage = mage.Damage;

            var mageSave = false;

            var numberOfEnemies = 10;

            var r = new Random();

            if (localHealth < 1)
            {
                

                if (mage.Resurrection)
                {
                    Console.WriteLine(" The gods smile upon you and decide to let you ressurect once to continue fighting.\n" +
                        " Will you take this offer?\n" +
                        " 1\t Yes!\n" +
                        " 2\t No, I'm done with this life.");

                    var mageRessurectChoice = IntegerInput.IntInputAndCheck(1, 2);

                    if (mageRessurectChoice == 1)
                    {
                        mageSave = true;
                    }

                }

                Console.Clear();

            }

            else
            {
                ColorPrints.ColorCyan("\n\n\t THE HERO HAS WON.\n\n");

            }
            return false;
        }



        public static bool BattleSimulatorRanger(Ranger ranger)
        {


            return false;
        }



        public static bool DeathScreen(int i)
        {
            ColorPrints.ColorRed("\n\n\tYOU DIED.\n\n");

            Console.WriteLine("\n Your hero's HP dropped to 0.\n" +
                $" Your run ended with {i} monster(s) slain.\n" +
                $" Try again?\n" +
                $" 1\t Try again!\n" +
                $" 2\t I'm done for today\n");

            var gameChoice = IntegerInput.IntInputAndCheck(1, 2);

            switch (gameChoice)
            {
                case 1:
                    return true;
                case 2:
                    return false;
            }

            return false;
        }


        public static bool WinScreen(int i)
        {
            ColorPrints.ColorGreen("\n\n\tTHE HERO HAS WON!\n\n");

            Console.WriteLine("Congratulations!\n\n" +
                "Against all odds, you came out on top.\n" +
                "Go treat yourself :)\n\n" +
                "Thanks for playing!");

            Console.WriteLine(" Press any key to exit the game");
            Console.ReadKey();

            Console.Clear();


            return true;
        }



        public static int MonsterDefeated(Hero hero, int monsterIndex)
        {
            int additionalMonsters = 0;

            Console.WriteLine("\n You've defeated the monster!\n");

            if(DataStore.AllMonsters[monsterIndex] is Witch witch)
            {
                Console.WriteLine(" The Witch chanted a summoning spell just as she took her final breath.\n" +
                    " Two additional, even stronger monsters now stand in your way!\n");

                RandomEnemy.CreateRandomEnemy(10);
                RandomEnemy.CreateRandomEnemy(20);

                additionalMonsters += 2;
            }

            if (hero.CurrentHealth + hero.Health / 4 > hero.Health)
            {
                hero.CurrentHealth = hero.Health;
            }

            else
            {
                hero.CurrentHealth += hero.Health / 4;
            }

            Console.WriteLine(" Your health has been restored by 25%\n\n" +
                $" Current Health:\t {hero.CurrentHealth}\n" +
                $" Max Health:\t\t {hero.Health}");

            if(hero.CurrentHealth != hero.Health)
            {
                Console.WriteLine("\n Would you like to forfeit this battle's experience to regain all of your HP?\n" +
               " 1\t Yes, heal me!\n" +
               " 2\t No, I can go on without resorting to such measures.\n");

                var healChoice = IntegerInput.IntInputAndCheck(1, 2);

                if(healChoice == 1)
                {
                    hero.CurrentHealth = hero.Health;
                    DataStore.AllMonsters[monsterIndex].Experience = 0;
                    Console.WriteLine("\n You're now at full HP!\n");
                }
            }

            hero.Experience += DataStore.AllMonsters[monsterIndex].Experience;

            LevelUpHelper.LevelUp(hero);

            return additionalMonsters;

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

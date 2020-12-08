using DungeonCrawler.Data;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Data.Models.Monsters;
using DungeonCrawler.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Services
{
    public class RandomEnemy
    {
        public static Monster CreateRandomEnemy(int additionalStats)
        {
            var enemyList = new List<Monster>
            {
                new Goblin(),
                new Brute(),
                new Witch()
            };

            var randomEnemyValue = StaticRandom.GetRandom(1, 10);

            var chosenEnemy = -1;

            if(randomEnemyValue < 6)
            {
                chosenEnemy = 0;

                enemyList[0].Health = StaticRandom.GetRandom(30, 40 + additionalStats);
                enemyList[0].CurrentHealth = enemyList[0].Health;
                enemyList[0].Damage = StaticRandom.GetRandom(5, 15);
                enemyList[0].Experience = StaticRandom.GetRandom(10, 15);
            }

            else if(randomEnemyValue < 8)
            {
                chosenEnemy = 1;

                enemyList[1].Health = StaticRandom.GetRandom(40, 60);
                enemyList[1].CurrentHealth = enemyList[1].Health;
                enemyList[1].Damage = StaticRandom.GetRandom(25, 35 + additionalStats);
                enemyList[1].Experience = StaticRandom.GetRandom(30, 40);
            }

            else
            {
                chosenEnemy = 2;

                enemyList[2].Health = StaticRandom.GetRandom(50, 75 + additionalStats);
                enemyList[2].CurrentHealth = enemyList[2].Health;
                enemyList[2].Damage = StaticRandom.GetRandom(40, 60 + additionalStats);
                enemyList[2].Experience = StaticRandom.GetRandom(40, 65);
            }

            DataStore.AllMonsters.Add(enemyList[chosenEnemy]);

            return enemyList[chosenEnemy];
        }


        public static int EnemyAction(Hero hero, Monster monster)
        {
            var r = new Random();

            var damageDealt = 0;

            if (monster is Goblin goblin)
            {
                damageDealt = StaticRandom.GetRandom(goblin.Damage-5, goblin.Damage+5);
                Console.WriteLine($" The Goblin stabbed you with his dagger, lowering your HP by {damageDealt}\n");
            }

            else if (monster is Brute brute)
            {
                var attackType = StaticRandom.GetRandom(1, 100);

                if (attackType < 76)
                {
                    damageDealt = StaticRandom.GetRandom(brute.Damage - 2, brute.Damage + 3);
                    Console.WriteLine($" The Brute scarped you with his axe, lowering your HP by {damageDealt}!\n");
                }

                else
                {
                    damageDealt = hero.Health / 5;
                    Console.WriteLine(" The Brute swung his axe into your body and cut off 20% of your total HP!\n");
                }
            }

            else if (monster is Witch witch)
            {
                var attackChoice = StaticRandom.GetRandom(1, 100);

                if (attackChoice < 60)
                {
                    damageDealt = StaticRandom.GetRandom(witch.Damage - 2, witch.Damage + 3);

                    Console.WriteLine($" The Witch hit you with an energy blast, dealing {damageDealt} damage!\n");
                }

                else
                {
                    Console.WriteLine(" The Witch twisted the HP of everything around her!\n");
                    if(hero.Health > 10)
                    {
                        hero.Health = StaticRandom.GetRandom(5, 150);
                        hero.CurrentHealth = hero.Health;

                        witch.Health = StaticRandom.GetRandom(1, 100);

                        var gotWitch = false;

                        foreach(var someMonster in DataStore.AllMonsters)
                        {
                            if(someMonster == witch)
                            {
                                gotWitch = true;
                            }

                            else if(gotWitch)
                            {
                                someMonster.Health = r.Next(1, someMonster.Health + 10);
                            }
                        }
                    }
                }
            }

            return damageDealt;
        }
    }
}

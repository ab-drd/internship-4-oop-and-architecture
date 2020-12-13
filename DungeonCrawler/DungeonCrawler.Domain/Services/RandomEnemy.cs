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
        public static void CreateRandomEnemy(int additionalStats)
        {
            var randomNumber = StaticRandom.GetRandom(1, 10);

            if(randomNumber < 6)
            {
                var randomGoblin = new Goblin
                {
                    Health = StaticRandom.GetRandom(30, 40 + additionalStats),
                    Damage = StaticRandom.GetRandom(5, 15),
                    Experience = StaticRandom.GetRandom(15, 20 + additionalStats*2)
                };

                randomGoblin.CurrentHealth = randomGoblin.Health;
            }

            else if(randomNumber < 9)
            {
                var randomBrute = new Brute
                {
                    Health = StaticRandom.GetRandom(40, 60),
                    Damage = StaticRandom.GetRandom(25, 35 + additionalStats),
                    Experience = StaticRandom.GetRandom(30, 40 + additionalStats * 2)
                };

                randomBrute.CurrentHealth = randomBrute.Health;
            }

            else
            {
                var randomWitch = new Witch
                {
                    Health = StaticRandom.GetRandom(50, 75 + additionalStats),
                    Damage = StaticRandom.GetRandom(40, 60 + additionalStats),
                    Experience = StaticRandom.GetRandom(40, 65 + additionalStats * 2)
                };

                randomWitch.CurrentHealth = randomWitch.Health;

            }
        }



        public static int MonsterAction(Hero hero, Monster monster)
        {
            var damageDealt = 0;

            if (monster is Goblin goblin)
            {
                damageDealt = goblin.Damage + StaticRandom.GetRandom(-5, 5);
                Console.WriteLine($" The Goblin stabbed you with his dagger, lowering your HP by {damageDealt}\n");
            }

            else if (monster is Brute brute)
            {
                var attackType = StaticRandom.GetRandom(1, 100);

                if (attackType < 76)
                {
                    damageDealt = brute.Damage + StaticRandom.GetRandom(-2, 2);
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

                        for(var i = DataStore.AllMonsters.IndexOf(witch); i < DataStore.AllMonsters.Count; i++)
                        {
                            DataStore.AllMonsters[i].Health = StaticRandom.GetRandom(5, 100);
                            DataStore.AllMonsters[i].CurrentHealth = DataStore.AllMonsters[i].Health;
                        }

                        
                    }
                }
            }

            return damageDealt;
        }



        public static void WitchEnemySummon()
        {
            Console.WriteLine(" The Witch chanted a summoning spell just as she took her final breath.\n" +
                                  " Two strong monsters appeared somewhere in the dungeon!\n");

            CreateRandomEnemy(10);
            CreateRandomEnemy(20);
        }
    }
}

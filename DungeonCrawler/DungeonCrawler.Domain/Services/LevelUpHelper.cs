﻿using DungeonCrawler.Data;
using DungeonCrawler.Data.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Domain.Services
{
    public static class LevelUpHelper
    {
        public static void LevelUp(Hero hero)
        {
            var levelledUp = false;

            var stopCondition = false;
            while(!stopCondition)
            {
                if (hero.Experience >= DataStore.ExperienceLevels[hero.Level])
                {
                    levelledUp = true;

                    Console.WriteLine($"\n\t >->->-*.**-*. You levelled up! {hero.Level} -> {hero.Level + 1} .*-*.**-<-<-<\n");

                    hero.Experience -= DataStore.ExperienceLevels[hero.Level];

                    hero.Health += hero.Level * 5;
                    hero.CurrentHealth += hero.Level * 5;
                    hero.Damage += hero.Level * 2;
                    
                    if (hero is Mage mage)
                    {
                        mage.Mana += hero.Level * 5;
                        mage.CurrentMana += hero.Level * 5;

                        if(mage.ManaAttackCost > 0)
                        {
                            mage.ManaAttackCost -= 5;
                        }

                        if(mage.ManaHealCost > 0)
                        {
                            mage.ManaHealCost -= 5;
                        }
                        
                        mage.ManaHealAmount += 5;
                    }

                    if(hero is Ranger ranger)
                    {
                        ranger.CriticalChance += hero.Level * 2;
                        ranger.StunChance += hero.Level * 1;
                    }

                    hero.Level++;
                }

                else
                {
                    stopCondition = true;
                }
                
            }

            if(levelledUp)
            {
                Console.WriteLine($" Your new stats are:\n\n"
                        + hero);
            }

            else
            {
                Console.WriteLine($"\n Experience\t\t {hero.Experience}/{DataStore.ExperienceLevels[hero.Level]}");
            }
            
        }
    }
}

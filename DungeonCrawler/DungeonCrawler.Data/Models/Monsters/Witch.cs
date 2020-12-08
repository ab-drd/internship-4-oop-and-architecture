using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;
using DungeonCrawler.Data.Enums;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Witch : Monster
    {
        public Witch()
        {
            MonsterType = MonsterTypes.Witch;
        }

        public static void DeathCry(int health)
        {
            if (health < 1)
            {
                
            }
        }
    }
}

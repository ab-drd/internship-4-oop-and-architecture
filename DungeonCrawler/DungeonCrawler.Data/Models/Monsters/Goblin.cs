using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;
using DungeonCrawler.Data.Enums;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Goblin : Monster
    {
        public Goblin()
        {
            MonsterType = MonsterTypes.Goblin;
        }
    }
}

using DungeonCrawler.Data.Abstractions;
using DungeonCrawler.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Monster : IHasHealth
    {
        public Monster()
        {
            DataStore.AllMonsters.Add(this);
        }

        public int Health { get; set; }

        public int CurrentHealth { get; set; }

        public int Damage { get; set; }

        public int Experience { get; set; }

        public MonsterTypes MonsterType { get; set; }

        public override string ToString()
        {
            return $"\tMonster\t\t\t {MonsterType}\n" +
                $"\tHealth\t\t\t {CurrentHealth}/{Health}\n" +
                $"\tDamage\t\t\t {Damage}\n";
        }

    }
}

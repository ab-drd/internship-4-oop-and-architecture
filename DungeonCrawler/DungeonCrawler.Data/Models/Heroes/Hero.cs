using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;
using DungeonCrawler.Data.Enums;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Hero : IHasHealth
    {
        public Hero() { }

        public string HeroName { get; set; }

        public int Health { get; set; }

        public int CurrentHealth { get; set; }

        public int Experience { get; set; }

        public int Damage { get; set; }

        public int Level { get; set; }

        public HeroClass HeroClass { get; set; }

        public override string ToString()
        {
            return $"\tName\t\t\t {HeroName}\n" +
                $"\tClass\t\t\t {HeroClass}\n\n" +
                $"\tLevel\t\t\t {Level}\n" +
                $"\tExperience\t\t {Experience}\n\n" +
                $"\tHealth\t\t\t {CurrentHealth}/{Health}\n" +
                $"\tDamage\t\t\t {Damage}\n\n";
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Ranger : Hero
    {
        public Ranger()
        {

        }

        public Ranger(string heroName) : base(heroName)
        {
            Health = 75;
            CurrentHealth = 75;
            Damage = 25;

            CriticalChance = 5;
            StunChance = 3;

        }

        public int CriticalChance { get; set; }

        public int StunChance { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"\tCritical Chance\t\t {CriticalChance}%\n" +
                $"\tStun Chance\t\t {StunChance}%";
        }
    }
}

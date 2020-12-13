using DungeonCrawler.Data.Enums;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Data.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data
{
    public static class DataStore
    {
        public static List<Monster> AllMonsters { get; } = new List<Monster>();

        public static Dictionary<int, int> ExperienceLevels = new Dictionary<int, int>
        {
            {1, 30},
            {2, 50},
            {3, 80},
            {4, 130},
            {5, 200},
            {6, 250},
            {7, 300},
            {8, 400},
            {9, 500},
            {10, 1000}
        };
    }
}

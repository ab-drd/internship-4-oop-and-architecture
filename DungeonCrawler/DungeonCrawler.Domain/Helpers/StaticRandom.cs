using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DungeonCrawler.Domain.Helpers
{
    public static class StaticRandom
    {
        public static int GetRandom(int min, int max)
        {
            return ( Math.Abs(Guid.NewGuid().GetHashCode()) % (max - min + 1) + min);
        }
    }
}

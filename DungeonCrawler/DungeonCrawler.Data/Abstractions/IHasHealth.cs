using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Abstractions
{
    public interface IHasHealth
    {
        int Health { get; set; }
    }
}

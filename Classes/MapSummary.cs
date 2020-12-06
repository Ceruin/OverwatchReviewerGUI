using System;
using System.Collections.Generic;
using System.Text;

namespace OverwatchReviewerGUI.Classes
{
    public class MapSummary
    {
        public string Map { get; private set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public ICollection<int> Kills { get; set; } = new List<int>();
        public ICollection<int> Deaths { get; set; } = new List<int>();

        public MapSummary(string Map)
        {
            this.Map = Map;
        }
    }
}

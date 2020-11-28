using System;
using System.Collections.Generic;
using System.Text;

namespace OverwatchReviewerGUI
{
    public class OcrSummary
    {
        public List<int> Deaths { get; set; } = new List<int>();
        public List<int> Kills { get; set; } = new List<int>();
        public string Map { get; set; }
        public List<bool> Victory { get; set; } = new List<bool>();
    }
}

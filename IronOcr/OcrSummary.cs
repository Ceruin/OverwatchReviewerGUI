using System.Collections.Generic;

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
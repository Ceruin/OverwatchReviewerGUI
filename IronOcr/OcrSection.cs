﻿using IronOcr;

namespace OverwatchReviewerGUI
{
    public class OcrSection
    {
        public OcrResult Deaths { get; set; }
        public OcrResult Kills { get; set; }
        public OcrResult Map { get; set; }
        public OcrResult Victory { get; set; }
    }
}
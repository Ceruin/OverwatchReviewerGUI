using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace OverwatchReviewerGUI
{
    public static class FileLibrary
    {
        #region Directories
        public const string outputDir = @"\owr_output";
        public const string inputDir = @"\owr_input";
        public const string processDir = @"\owr_processing";
        public const string minimeDir = @"\owr_minime";
        public const string finishedDir = @"\owr_finished";       
        #endregion Directories

        #region Files
        public const string outputFile = @"MapSummaries.txt";
        public const string minimeFile = @"\minime";
        #endregion Files

        #region FileExtensions
        public const string jpg = @".jpg";
        #endregion FileExtensions

    }
}

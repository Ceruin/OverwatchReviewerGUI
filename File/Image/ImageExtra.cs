using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace OverwatchReviewerGUI.File.Image
{
    public class ImageExtra
    {
        public System.Drawing.Image image { get; private set; }
        public string imagePath { get; private set; }

        public ImageExtra(System.Drawing.Image image, string imagePath)
        {
            this.image = image;
            this.imagePath = imagePath;
        }
    }
}

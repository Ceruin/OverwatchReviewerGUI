using System;
using System.Drawing;

namespace OverwatchReviewerGUI
{
    public static class ImageExtensions
    {
        public static void VisualizeRectangle(Image image, Rectangle rectangle)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawRectangle(new Pen(Color.Red), rectangle);
            }
        }
    }
}
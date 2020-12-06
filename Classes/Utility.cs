using System;

namespace OverwatchReviewerGUI
{
    public static class Utility
    {
        public static int ScaleValue_BasedOnMaxResolution(float percentDifference, int value)
        {
            return (int)MathF.Round(percentDifference * value);
        }

        public static float GetPercentDifference(float valueOne, float valueTwo)
        {
            return valueOne / valueTwo;
        }
    }
}
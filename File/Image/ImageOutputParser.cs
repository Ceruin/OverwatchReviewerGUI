using System;
using System.Collections.Generic;

namespace OverwatchReviewerGUI.File.Image
{
    public static class ImageOutputParser
    {
        public enum OutputType
        {
            Victory,
            Map,
            Kills,
            Deaths
        }

        public static string ParseString(List<string> output, OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.Map:
                    var boxText = output[0].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    return boxText[0];

                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static int ParseInt(List<string> output, OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.Kills:
                    return output[1] == null ? 0 : int.Parse(output[1].ToUpper().Replace("O", "0").Replace("°", "")); // this is the ^ ow does

                case OutputType.Deaths:
                    return output[1] == null ? 0 : int.Parse(output[1].ToUpper().Replace("O", "0").Replace("°", "")); // this is the ^ ow does

                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static bool ParseBool(List<string> output, OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.Victory:
                    var allText = output[0];
                    var boxText
                        = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    return boxText[1].ToUpper().Contains("VIC");

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
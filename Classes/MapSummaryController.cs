using OverwatchReviewerGUI.File.Image;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OverwatchReviewerGUI.Classes
{
    public static class MapSummaryController
    {
        public static ICollection<MapSummary> GetMapSummaries(ICollection<ImageSummary> imageSummaries)
        {
            List<MapSummary> mapSummaries = new List<MapSummary>();
            foreach (var summary in imageSummaries)
            {
                // Get the Maps and Combine the Map Data
                var activeMapSummary = mapSummaries.FirstOrDefault(p => p.Map == summary.Map);
                if (activeMapSummary == null)
                {
                    MapSummary newMapSummary = new MapSummary(summary.Map);
                    ImageSummaryToMapSummary(summary, newMapSummary);
                    mapSummaries.Add(newMapSummary);
                }
                else
                {
                    ImageSummaryToMapSummary(summary, activeMapSummary);
                }
            }
            return mapSummaries;
        }

        private static void ImageSummaryToMapSummary(ImageSummary summary, MapSummary newMapSummary)
        {
            switch (summary.Victory)
            {
                case true:
                    newMapSummary.Victories++;
                    break;
                case false:
                    newMapSummary.Defeats++;
                    break;
            }
            newMapSummary.Kills.Add(summary.Kills);
            newMapSummary.Deaths.Add(summary.Deaths);
        }
    }
}
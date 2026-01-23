using System;
using SkiaSharp;
using Wacton.Unicolour;

namespace pixel8r.Helpers
{
    public class PaletteMatchingHelper
    {
        public static SKColor getMatchedColor(SKColor color, string algorithm)
        {
            if (algorithm == "RGB Euclidean")
            {
                return getNearestBySystemColorDelta(color, getRGBEuclideanDiff);
            }
            if (algorithm == "RGB Redmean")
            {
                return getNearestBySystemColorDelta(color, getRGBRedmeanDiff);
            }
            if (algorithm == "Lab CIE76")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Cie76);
            }
            if (algorithm == "Lab Hybrid")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Hyab);
            }
            if (algorithm == "Lab CIE94")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Cie94);
            }
            if (algorithm == "LCh CIEDE2000")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Ciede2000);
            }
            if (algorithm == "CMC Acceptability")
            {
                return getNearestByUnicolourDelta(color, DeltaE.CmcAcceptability);
            }
            if (algorithm == "CMC Perceptibility")
            {
                return getNearestByUnicolourDelta(color, DeltaE.CmcPerceptibility);
            }
            if (algorithm == "ITP")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Itp);
            }
            if (algorithm == "Z")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Z);
            }
            if (algorithm == "OK")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Ok);
            }
            if (algorithm == "CAM02")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Cam02);
            }
            if (algorithm == "CAM16")
            {
                return getNearestByUnicolourDelta(color, DeltaE.Cam16);
            }
            // default case, should not be reachable
            return color;
        }

        private static SKColor getNearestBySystemColorDelta(SKColor color, Func<SKColor, SKColor, double> deltaFunc)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
            for (int i = 0; i < GlobalVars.CurrentPalette.Count; i++)
            {
                double deltaE = deltaFunc(color, GlobalVars.CurrentPalette[i]);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, GlobalVars.CurrentPalette[colorIndex]);
            return GlobalVars.CurrentPalette[colorIndex];
        }

        private static SKColor getNearestByUnicolourDelta(SKColor color, DeltaE deltaEnum)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
            Unicolour unicolour = ColorConversionHelper.getUnicolourFromSKColor(color);
            Unicolour[] comparisonPalette = ColorConversionHelper.getUnicoloursFromSKColors(GlobalVars.CurrentPalette);
            for (int i = 0; i < GlobalVars.CurrentPalette.Count; i++)
            {
                double deltaE = unicolour.Difference(comparisonPalette[i], deltaEnum);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, GlobalVars.CurrentPalette[colorIndex]);
            return GlobalVars.CurrentPalette[colorIndex];
        }

        private static double getRGBEuclideanDiff(SKColor color1, SKColor color2)
        {
            return Math.Sqrt(Math.Pow(color1.Red - color2.Red, 2) + Math.Pow(color1.Green - color2.Green, 2) + Math.Pow(color1.Blue - color2.Blue, 2));
        }

        private static double getRGBRedmeanDiff(SKColor color1, SKColor color2)
        {
            double r = 0.5 * (color1.Red + color2.Red);
            double weightedDeltaR = (2 + r / 256) * Math.Pow(color1.Red - color2.Red, 2);
            double weightedDeltaG = 4 * Math.Pow(color1.Green - color2.Green, 2);
            double weightedDeltaB = (2 + (255 - r) / 256) * Math.Pow(color1.Blue - color2.Blue, 2);
            return Math.Sqrt(weightedDeltaR + weightedDeltaG + weightedDeltaB);
        }
    }
}

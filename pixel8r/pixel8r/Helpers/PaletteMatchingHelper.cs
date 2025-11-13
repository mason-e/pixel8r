using System;
using SkiaSharp;
using Wacton.Unicolour;

namespace pixel8r.Helpers
{
    public class PaletteMatchingHelper
    {
        public static SKColor getMatchedColor(SKColor color, string palette, string algorithm)
        {
            if (algorithm == "RGB Euclidean")
            {
                return getNearestBySystemColorDelta(color, palette, getRGBEuclideanDiff);
            }
            if (algorithm == "RGB Redmean")
            {
                return getNearestBySystemColorDelta(color, palette, getRGBRedmeanDiff);
            }
            if (algorithm == "Lab CIE76")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Cie76);
            }
            if (algorithm == "Lab Hybrid")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Hyab);
            }
            if (algorithm == "Lab CIE94")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Cie94);
            }
            if (algorithm == "LCh CIEDE2000")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Ciede2000);
            }
            if (algorithm == "CMC Acceptability")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.CmcAcceptability);
            }
            if (algorithm == "CMC Perceptibility")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.CmcPerceptibility);
            }
            if (algorithm == "ITP")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Itp);
            }
            if (algorithm == "Z")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Z);
            }
            if (algorithm == "OK")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Ok);
            }
            if (algorithm == "CAM02")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Cam02);
            }
            if (algorithm == "CAM16")
            {
                return getNearestByUnicolourDelta(color, palette, DeltaE.Cam16);
            }
            // default case, should not be reachable
            return color;
        }

        private static SKColor getNearestBySystemColorDelta(SKColor color, string palette, Func<SKColor, SKColor, double> deltaFunc)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
            SKColor[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = Constants.mesenColors;
            }
            for (int i = 0; i < targetPalette.Length; i++)
            {
                double deltaE = deltaFunc(color, targetPalette[i]);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, targetPalette[colorIndex]);
            return targetPalette[colorIndex];
        }

        private static SKColor getNearestByUnicolourDelta(SKColor color, string palette, DeltaE deltaEnum)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
            Unicolour unicolour = ColorConversionHelper.getUnicolourFromSKColor(color);
            SKColor[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = Constants.mesenColors;
            }
            Unicolour[] comparisonPalette = ColorConversionHelper.getUnicoloursFromSKColors(targetPalette);
            for (int i = 0; i < targetPalette.Length; i++)
            {
                double deltaE = unicolour.Difference(comparisonPalette[i], deltaEnum);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, targetPalette[colorIndex]);
            return targetPalette[colorIndex];
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

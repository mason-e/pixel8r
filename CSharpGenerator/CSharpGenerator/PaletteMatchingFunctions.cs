using Wacton.Unicolour;

namespace CSharpGenerator
{
    internal class PaletteMatchingFunctions
    {
        public static Color getMatchedColor(Color oldColor, string palette, string algorithm)
        {
            if (algorithm == "RGB Euclidean")
            {
                return findNearestColorRGBEuclidean(oldColor, palette);
            }
            if (algorithm == "RGB Redmean")
            {
                return findNearestColorRGBRedmean(oldColor, palette);
            }
            if (algorithm == "Lab Value Difference")
            {
                return findNearestColorLab(oldColor, palette);
            }
            // default case, should not be reachable
            return oldColor;
        }

        private static Color findNearestColorRGBEuclidean(Color oldColor, string palette)
        {
            // based on https://en.wikipedia.org/wiki/Color_difference#sRGB
            double largestDiff = 441.673; // this is the square root of the sum of 255 squared three times, maximum diff any RGB color could have
            int colorIndex = 0;
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            for (int i = 0; i < targetPalette.Length; i++)
            {
                Color compare = targetPalette[i];
                double totalDiff = Math.Sqrt(Math.Pow(compare.R - oldColor.R, 2) + Math.Pow(compare.G - oldColor.G, 2) + Math.Pow(compare.B - oldColor.B, 2));
                if (totalDiff < largestDiff)
                {
                    largestDiff = totalDiff;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }

        private static Color findNearestColorRGBRedmean(Color oldColor, string palette)
        {
            // based on https://en.wikipedia.org/wiki/Color_difference#sRGB - "redmean" formula
            // delta G and delta B max = 255,
            // I *think* delta R = 255 would produce the max, since it's a squared value, which makes r = 127.5
            // so, put together: square root of 2.498 * 255^2 + 4 * 255^2 + 2.498 * 255^2 or we can simplify to sqrt(9 * 255^2) = 765
            double largestDiff = 765;
            int colorIndex = 0;
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            for (int i = 0; i < targetPalette.Length; i++)
            {
                Color compare = targetPalette[i];
                double r = 0.5 * (compare.R + oldColor.R);
                double weightedDeltaR = (2 + r / 256) * Math.Pow(compare.R - oldColor.R, 2);
                double weightedDeltaG = 4 * Math.Pow(compare.G - oldColor.G, 2);
                double weightedDeltaB = (2 + (255 - r) / 256) * Math.Pow(compare.B - oldColor.B, 2);
                double totalDiff = Math.Sqrt(weightedDeltaR + weightedDeltaG + weightedDeltaB);
                if (totalDiff < largestDiff)
                {
                    largestDiff = totalDiff;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }

        private static Color findNearestColorLab(Color oldColor, string palette)
        {
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = 0;
            Unicolour unicolor = ColorConversionFunctions.getUnicolorFromSystemColor(oldColor);
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            Unicolour[] comparisonPalette = ColorConversionFunctions.getUnicolorsFromSystemColors(targetPalette);
            for (int i = 0; i < targetPalette.Length; i++)
            {
                double deltaE = unicolor.Difference(comparisonPalette[i], DeltaE.Cie76);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }
    }
}

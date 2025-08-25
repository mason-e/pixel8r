using Wacton.Unicolour;

namespace pixel8r
{
    public class PaletteMatchingFunctions
    {
        public static Color getMatchedColor(Color oldColor, string palette, string algorithm, bool dither)
        {
            if (algorithm == "RGB Euclidean")
            {
                return getNearestBySystemColorDelta(oldColor, palette, getRGBEuclideanDiff, dither);
            }
            if (algorithm == "RGB Redmean")
            {
                return getNearestBySystemColorDelta(oldColor, palette, getRGBRedmeanDiff, dither);
            }
            if (algorithm == "Lab CIE76")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Cie76, dither);
            }
            if (algorithm == "Lab Hybrid")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Hyab, dither);
            }
            if (algorithm == "Lab CIE94")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Cie94, dither);
            }
            if (algorithm == "LCh CIEDE200")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Ciede2000, dither);
            }
            if (algorithm == "CMC Acceptability")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.CmcAcceptability, dither);
            }
            if (algorithm == "CMC Perceptibility")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.CmcPerceptibility, dither);
            }
            if (algorithm == "ITP")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Itp, dither);
            }
            if (algorithm == "Z")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Z, dither);
            }
            if (algorithm == "OK")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Ok, dither);
            }
            if (algorithm == "CAM02")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Cam02, dither);
            }
            if (algorithm == "CAM16")
            {
                return getNearestByUnicolourDelta(oldColor, palette, DeltaE.Cam16, dither);
            }
            // default case, should not be reachable
            return oldColor;
        }

        private static Color getNearestBySystemColorDelta(Color color, string palette, Func<Color, Color, double> deltaFunc, bool dither)
        {
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            double deltaEMin2 = 10000;
            int closestIndex = -1;
            int secondClosestIndex = -1;
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
                double deltaE = deltaFunc(color, targetPalette[i]);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    // if we have a new lowest, push over the previous to second lowest
                    secondClosestIndex = closestIndex;
                    closestIndex = i;
                }
                // get difference from lowest to second lowest - this accounts for the scenario where the true lowest is the first one found
                else if (deltaE - deltaEMin < deltaEMin2)
                {
                    secondClosestIndex = i;
                    deltaEMin2 = deltaE - deltaEMin;
                }
            }

            if (dither)
            {
                return targetPalette[secondClosestIndex];
            }
            else
            {
                return targetPalette[closestIndex];
            }
        }

        private static Color getNearestByUnicolourDelta(Color color, string palette, DeltaE deltaEnum, bool dither)
        {
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            double deltaEMin2 = 10000;
            int closestIndex = -1;
            int secondClosestIndex = -1;
            Unicolour unicolour = ColorConversionFunctions.getUnicolourFromSystemColor(color);
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            Unicolour[] comparisonPalette = ColorConversionFunctions.getUnicoloursFromSystemColors(targetPalette);
            for (int i = 0; i < targetPalette.Length; i++)
            {
                double deltaE = unicolour.Difference(comparisonPalette[i], deltaEnum);
                if (deltaE < deltaEMin)
                {
                    deltaEMin = deltaE;
                    // if we have a new lowest, push over the previous to second lowest
                    secondClosestIndex = closestIndex;
                    closestIndex = i;
                }
                // get difference from lowest to second lowest - this accounts for the scenario where the true lowest is the first one found
                else if (deltaE - deltaEMin < deltaEMin2)
                {
                    secondClosestIndex = i;
                    deltaEMin2 = deltaE - deltaEMin;
                }
            }

            if (dither)
            {
                return targetPalette[secondClosestIndex];
            }
            else
            {
                return targetPalette[closestIndex];
            }
        }

        private static double getRGBEuclideanDiff(Color color1, Color color2)
        {
            return Math.Sqrt(Math.Pow(color1.R - color2.R, 2) + Math.Pow(color1.G - color2.G, 2) + Math.Pow(color1.B - color2.B, 2));
        }

        private static double getRGBRedmeanDiff(Color color1, Color color2)
        {
            double r = 0.5 * (color1.R + color2.R);
            double weightedDeltaR = (2 + r / 256) * Math.Pow(color1.R - color2.R, 2);
            double weightedDeltaG = 4 * Math.Pow(color1.G - color2.G, 2);
            double weightedDeltaB = (2 + (255 - r) / 256) * Math.Pow(color1.B - color2.B, 2);
            return Math.Sqrt(weightedDeltaR + weightedDeltaG + weightedDeltaB);
        }
    }
}

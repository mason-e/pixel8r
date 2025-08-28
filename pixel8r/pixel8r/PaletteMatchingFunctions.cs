using Wacton.Unicolour;

namespace pixel8r
{
    public class PaletteMatchingFunctions
    {
        public static Color getMatchedColor(Color color, string palette, string algorithm)
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

        private static Color getNearestBySystemColorDelta(Color color, string palette, Func<Color, Color, double> deltaFunc)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
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
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, targetPalette[colorIndex]);
            return targetPalette[colorIndex];
        }

        private static Color getNearestByUnicolourDelta(Color color, string palette, DeltaE deltaEnum)
        {
            if (GlobalVars.colorMatches.Keys.Contains(color))
            {
                return GlobalVars.colorMatches[color];
            }
            double deltaEMin = 10000; // set to a very high number that any delta can beat
            int colorIndex = -1;
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
                    colorIndex = i;
                }
            }
            GlobalVars.colorMatches.Add(color, targetPalette[colorIndex]);
            return targetPalette[colorIndex];
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

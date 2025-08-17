namespace CSharpGenerator
{
    internal class PaletteMatchingFunctions
    {
        public static Color getMatchedColor(Color oldColor, string palette, string algorithm)
        {
            if (algorithm == "RGB Simple Sum of Diffs")
            {
                return findNearestColorRGBSimpleSumDiffs(oldColor, palette);
            }
            if (algorithm == "RGB Square Root of Summed Diff Squares")
            {
                return findNearestColorRGBSqrtOfSummedSquaredDiffs(oldColor, palette);
            }
            if (algorithm == "RGB Redmean")
            {
                return findNearestColorRGBRedmean(oldColor, palette);
            }
            if (algorithm == "HSL Difference")
            {
                return findNearestColorHSL(oldColor, palette);
            }
            if (algorithm == "Lab Value Difference")
            {
                return findNearestColorLab(oldColor, palette);
            }
            // default case, should not be reachable
            return oldColor;
        }

        private static Color findNearestColorRGBSimpleSumDiffs(Color oldColor, string palette)
        {
            int largestDiff = 255 * 3;
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
                int totalDiff = Math.Abs(compare.R - oldColor.R) + Math.Abs(compare.G - oldColor.G) + Math.Abs(compare.B - oldColor.B);
                if (totalDiff < largestDiff)
                {
                    largestDiff = totalDiff;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }

        private static Color findNearestColorRGBSqrtOfSummedSquaredDiffs(Color oldColor, string palette)
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
            (double, double, double) labColor = AsposeFunctions.getLabColor(oldColor.R, oldColor.G, oldColor.B);
            // square root of 100^2 + 255^2 + 255^2
            double lowestDiff = 374.23;
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
                Color compareColor = targetPalette[i];
                (double, double, double) labCompare = AsposeFunctions.getLabColor(compareColor.R, compareColor.G, compareColor.B);
                double totalDiff = Math.Sqrt(Math.Pow(labCompare.Item1 - labColor.Item1, 2) + Math.Pow(labCompare.Item2 - labColor.Item2, 2) + Math.Pow(labCompare.Item3 - labColor.Item3, 2));
                if (totalDiff < lowestDiff)
                {
                    lowestDiff = totalDiff;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }

        private static Color findNearestColorHSL(Color oldColor, string palette)
        {
            double hue = oldColor.GetHue();
            double saturation = oldColor.GetSaturation();
            double lightness = oldColor.GetBrightness();
            double highestAverageDiff = Math.Sqrt(3);
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
                double hueDiff = Math.Min(Math.Abs(compare.GetHue() - hue), 360 - Math.Abs(compare.GetHue() - hue)) / 180;
                double satDiff = Math.Abs(compare.GetSaturation() - saturation);
                double lightDiff = Math.Abs(compare.GetBrightness() - lightness);
                double diffSum = Math.Sqrt(hueDiff * hueDiff + satDiff * satDiff + lightDiff * lightDiff);
                if (diffSum < highestAverageDiff)
                {
                    highestAverageDiff = diffSum;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }
    }
}

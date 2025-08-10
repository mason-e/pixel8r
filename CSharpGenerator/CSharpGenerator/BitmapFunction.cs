namespace CSharpGenerator
{
    internal class BitmapFunction
    {
        public static Bitmap generateBitmap()
        {
            Bitmap original = new Bitmap(GlobalVars.FilePath);
            if (original.Width > 1118 || original.Height > 720)
            {
                float scaleFactor = Math.Max((float)original.Width / 1118, (float)original.Height / 720);
                return resize(original, scaleFactor);
            }
            else
            {
                GlobalVars.ImageWidth = original.Width;
                GlobalVars.ImageHeight = original.Height;
                return original;
            }
        }

        public static Bitmap resize(Bitmap original, float scaleFactor)
        {
            GlobalVars.ImageWidth = (int)(original.Width / scaleFactor);
            GlobalVars.ImageHeight = (int)(original.Height / scaleFactor);
            return new Bitmap(original, new Size(GlobalVars.ImageWidth, GlobalVars.ImageHeight));
        }

        public static Bitmap paletteSwapDrawing(Image image, string palette, string algorithm)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color newColor = getNewColor(bitmap.GetPixel(x, y), palette, algorithm);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            return bitmap;
        }

        public static Bitmap dither(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            int x = 1, y = 1;
            while (x < bitmap.Width && y < bitmap.Height)
            {
                bitmap.SetPixel(x, y, Color.White);
                if (x + 3 < bitmap.Width)
                {
                    x += 3;
                }
                else if (y + 3 < bitmap.Height)
                {
                    y += 3;
                    x = 1;
                }
                else break;
            }
            return bitmap;
        }

        public static Bitmap pixelate(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            int x = 1, y = 1;
            Random r = new Random();
            while (x < bitmap.Width && y < bitmap.Height)
            {
                // choose one color in the cluster of 3x3 at random, i.e. a pixel that is -1, 0 or 1 from current pixel
                int colorX = x + r.Next(-1, 2);
                int colorY = y + r.Next(-1, 2);
                Color color = bitmap.GetPixel(colorX, colorY);
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        try
                        {
                            bitmap.SetPixel(x + i, y + j, color);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {

                        }
                    }
                }
                if (x + 3 < bitmap.Width)
                {
                    x += 3;
                }
                else if (y + 3 < bitmap.Height)
                {
                    y += 3;
                    x = 1;
                }
                else break;
            }
            
                return bitmap;           
        }

        public static Color getNewColor(Color oldColor, string palette, string algorithm)
        {
            if (algorithm == "RGB Lowest Combined Linear Diff")
            {
                return findNearestColorRGBSmallestCombinedLinearDiff(oldColor, palette);
            }
            if (algorithm == "RGB Iterative Threshold of Diffs")
            {
                return findNearestColorRGBIterativeDiff(oldColor, palette);
            }
            if (algorithm == "RGB Lowest Square Root of Summed Diffs")
            {
                return findNearestColorRGBSqrtOfSummedDiffs(oldColor, palette);
            }
            if (algorithm == "RGB Redmean")
            {
                return findNearestColorRGBRedmean(oldColor, palette);
            }
            else
            {
                return grayScale(oldColor);
            }
            
        }

        public static Color findNearestColorRGBIterativeDiff(Color oldColor, string palette)
        {
            int acceptableDiff = 5;
            List<int> possibleIndices = [];
            int red = oldColor.R;
            int green = oldColor.G;
            int blue = oldColor.B;
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
            // first check if at least one value of RGB matches
            for (int i = 0; i < targetPalette.Length; i++)
            {
                Color compare = targetPalette[i];
                if (anyRGBMatch(compare, oldColor, acceptableDiff));
                {
                    possibleIndices.Add(i);
                }
                // if none found, increase the diff by 5 and loop again
                if (i == targetPalette.Length - 1 && possibleIndices.Count == 0)
                {
                    acceptableDiff += 5;
                    i = 0;
                }
            }
            
            // if above produced more than one result, check if at least two values of RGB match
            if (possibleIndices.Count > 1)
            {
                acceptableDiff = 5;
                possibleIndices.Clear();
                for (int i = 0; i < targetPalette.Length; i++)
                {
                    Color compare = targetPalette[i];
                    if (twoRGBMatch(compare, oldColor, acceptableDiff))
                    {
                        possibleIndices.Add(i);
                    }
                    // if none found, increase the diff by 5 and loop again
                    if (i == targetPalette.Length - 1 && possibleIndices.Count == 0)
                    {
                        acceptableDiff += 5;
                        i = 0;
                    }
                }
            }

            // if above produced more than one result, check if all three values of RGB match
            if (possibleIndices.Count > 1)
            {
                acceptableDiff = 5;
                possibleIndices.Clear();
                for (int i = 0; i < targetPalette.Length; i++)
                {
                    Color compare = targetPalette[i];
                    if (allRGBMatch(compare, oldColor, acceptableDiff))
                    {
                        possibleIndices.Add(i);
                    }
                    // if none found, increase the diff by 5 and loop again
                    if (i == targetPalette.Length - 1 && possibleIndices.Count == 0)
                    {
                        acceptableDiff += 5;
                        i = 0;
                    }
                }
            }

            // if we STILL have multiple results, get the result with the smallest total diff
            if (possibleIndices.Count > 1)
            {
                int largestDiff = 255 * 3;
                foreach (int index in possibleIndices) {
                    Color compare = targetPalette[index];
                    int totalDiff = Math.Abs(compare.R - red) + Math.Abs(compare.G - green) + Math.Abs(compare.B - blue);
                    if (totalDiff < largestDiff)
                    {
                        largestDiff = totalDiff;
                        colorIndex = index;
                    }
                }
            }
            else
            {
                colorIndex = possibleIndices[0];
            }

            return targetPalette[colorIndex];
        }

        private static bool anyRGBMatch(Color color1, Color color2, int difference)
        {
            return Math.Abs(color1.R - color2.R) <= difference || Math.Abs(color1.G - color2.G) <= difference || Math.Abs(color1.B - color2.B) <= difference;
        }

        private static bool twoRGBMatch(Color color1, Color color2, int difference)
        {
            return (Math.Abs(color1.R - color2.R) <= difference && Math.Abs(color1.G - color2.G) <= difference) ||
                (Math.Abs(color1.R - color2.R) <= difference && Math.Abs(color1.B - color2.B) <= difference) ||
                (Math.Abs(color1.G - color2.G) <= difference && Math.Abs(color1.B - color2.B) <= difference);
        }

        private static bool allRGBMatch(Color color1, Color color2, int difference)
        {
            return Math.Abs(color1.R - color2.R) <= difference && Math.Abs(color1.G - color2.G) <= difference && Math.Abs(color1.B - color2.B) <= difference;
        }

        public static Color findNearestColorRGBSmallestCombinedLinearDiff(Color oldColor, string palette)
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

        public static Color findNearestColorRGBSqrtOfSummedDiffs(Color oldColor, string palette)
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

        public static Color findNearestColorRGBRedmean(Color oldColor, string palette)
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

        public static Color grayScale(Color oldColor)
        {
            int grayScale = (int)((oldColor.R * 0.3) + (oldColor.G * 0.59) + (oldColor.B * 0.11));
            return Color.FromArgb(grayScale, grayScale, grayScale);
        }

        public static Color findNearestColorHSV(Color oldColor, string palette)
        {
            float hue = oldColor.GetHue();
            float saturation = oldColor.GetSaturation();
            float lightness = oldColor.GetBrightness();
            float highestAverageDiff = 1;
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
                float hueDiff = Math.Abs(compare.GetHue() - hue);
                float satDiff = Math.Abs(compare.GetSaturation() - saturation);
                float lightDiff = Math.Abs(compare.GetBrightness() - lightness);
                float averageDiff = (hueDiff / 360 + satDiff + lightDiff) / 3;
                if (averageDiff < highestAverageDiff)
                {
                    highestAverageDiff = averageDiff;
                    colorIndex = i;
                }
            }

            return targetPalette[colorIndex];
        }

        public static Bitmap drawPalette(string palette)
        {
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            Bitmap bitmap = new Bitmap(240, 192);
            int x = 0;
            int y = 0;
            // attempts to fill the preview area as much as possible based on the size of the palette
            // the total available pixels are 240*192 = 46,080, adjust side length to 8, 16, 24, 48 (common factors of 240 and 192)
            int rawTileSize = (int)Math.Sqrt(46080 / targetPalette.Length);
            int tileSize = rawTileSize > 48 ? 48 : (rawTileSize > 24 ? 24 : (rawTileSize > 16 ? 16 : (rawTileSize > 8 ? 8 : rawTileSize)));
            foreach (Color color in targetPalette)
            {
                // step down to next row if it would overflow the current row
                if (x + tileSize > 240)
                {
                    x = 0;
                    y += tileSize;
                }
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.FillRectangle(new SolidBrush(color), x, y, tileSize, tileSize);
                }
                x += tileSize;
            }
            return bitmap;
        }
    }
}

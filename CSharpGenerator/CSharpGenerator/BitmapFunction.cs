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

        public static Bitmap programaticallyPaletteSwap(Image image, string palette)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color newColor = getProgrammaticColor(bitmap.GetPixel(x, y), palette);
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
            else
            {
                // essentially do nothing
                return oldColor;
            }
        }

        public static Color getProgrammaticColor(Color oldColor, string palette)
        {
            if (palette == "Grayscale")
            {
                return grayScale(oldColor);
            }
            if (palette == "RGB Multiples of 3")
            {
                return findNearestRGBMultiple(oldColor, 3);
            }
            if (palette == "RGB Multiples of 5")
            {
                return findNearestRGBMultiple(oldColor, 5);
            }
            if (palette == "RGB Multiples of 15")
            {
                return findNearestRGBMultiple(oldColor, 15);
            }
            if (palette == "RGB Multiples of 17")
            {
                return findNearestRGBMultiple(oldColor, 17);
            }
            if (palette == "RGB Multiples of 51")
            {
                return findNearestRGBMultiple(oldColor, 51);
            }
            if (palette == "RGB Multiples of 85")
            {
                return findNearestRGBMultiple(oldColor, 85);
            }
            else
            {
                // essentially do nothing
                return oldColor;
            }
        }

        public static Color findNearestRGBMultiple(Color oldColor, int multiple)
        {
            double midPoint = (double)multiple / 2;
            int rModulo = oldColor.R % multiple;
            int gModulo = oldColor.G % multiple;
            int bModulo = oldColor.B % multiple;
            int newR, newG, newB;

            if (rModulo != 0)
            {
                if (rModulo < midPoint)
                {
                    newR = oldColor.R - rModulo;
                }
                else
                {
                    newR = oldColor.R + (multiple - rModulo);
                }
            }
            else
            {
                newR = oldColor.R;
            }
            if (gModulo != 0)
            {
                if (gModulo < midPoint)
                {
                    newG = oldColor.G - gModulo;
                }
                else
                {
                    newG = oldColor.G + (multiple - gModulo);
                }
            }
            else
            {
                newG = oldColor.G;
            }
            if (bModulo != 0)
            {
                if (bModulo < midPoint)
                {
                    newB = oldColor.B - bModulo;
                }
                else
                {
                    newB = oldColor.B + (multiple - bModulo);
                }
            }
            else
            {
                newB = oldColor.B;
            }
            return Color.FromArgb(newR, newG, newB);
        }

        public static Color findNearestColorRGBSimpleSumDiffs(Color oldColor, string palette)
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

        public static Color findNearestColorRGBSqrtOfSummedSquaredDiffs(Color oldColor, string palette)
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

        public static Color findNearestColorLab(Color oldColor, string palette)
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

        public static Color findNearestColorHSL(Color oldColor, string palette)
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

        public static Color grayScale(Color oldColor)
        {
            int grayScale = (int)((oldColor.R * 0.3) + (oldColor.G * 0.59) + (oldColor.B * 0.11));
            return Color.FromArgb(grayScale, grayScale, grayScale);
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

using System.Drawing.Imaging;
using System.IO;

namespace CSharpGenerator
{
    internal class BitmapFunction
    {
        public static Bitmap generateBitmap()
        {
            Bitmap original = new Bitmap(GlobalVars.FilePath1);
            if (original.Width > 1118 || original.Height > 720)
            {
                float scaleFactor = Math.Max((float)original.Width / 1118, (float)original.Height / 720);
                return resize(original, scaleFactor);
            }
            else
            {
                return original;
            }
        }

        public static Bitmap resize(Bitmap original, float scaleFactor)
        {
            return new Bitmap(original, new Size((int)(original.Width / scaleFactor), (int)(original.Height / scaleFactor)));
        }

        public static Bitmap pixelateDrawing(Image image, string palette, string algorithm)
        {
            Bitmap bitmap = (Bitmap)image;
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

        public static Color getNewColor(Color oldColor, string palette, string algorithm)
        {
            if (algorithm == "RGB Lowest Combined Diff")
            {
                return findNearestColorRGB(oldColor, palette);
            }
            if (algorithm == "HSV Lowest Combined Diff")
            {
                return findNearestColorHSV(oldColor, palette);
            }
            else
            {
                return grayScale(oldColor);
            }
            
        }

        public static Color findNearestColorRGB(Color oldColor, string palette)
        {
            int largestDiff = 255 * 3;
            int colorIndex = 0;
            int red = oldColor.R;
            int green = oldColor.G;
            int blue = oldColor.B;
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
                int totalDiff = Math.Abs(compare.R - red) + Math.Abs(compare.G - green) + Math.Abs(compare.B - blue);
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
    }
}

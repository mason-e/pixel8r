using System.IO;

namespace CSharpGenerator
{
    internal class BitmapFunction
    {
        public static Bitmap generateBitmap()
        {
            if (isImageFile())
            {
                return new Bitmap(GlobalVars.FilePath1);
            }
            else
            {
                byte[] fileData = new byte[GlobalVars.Size];
                using (FileStream f = File.OpenRead(GlobalVars.FilePath1))
                {
                    f.Read(fileData, 0, GlobalVars.Size);
                }
                int sideLength = (int)Math.Sqrt(GlobalVars.Size / 3);
                Bitmap bitmap = new Bitmap(sideLength, sideLength);

                int xCoord = 0;
                int yCoord = 0;
                for (int i = 0; i + 2 < GlobalVars.Size; i += 3)
                {
                    // to account for decimal truncation issues when file size is not divisible by 3
                    if (yCoord < sideLength)
                    {
                        bitmap.SetPixel(xCoord, yCoord, Color.FromArgb(fileData[i + 2], fileData[i + 1], fileData[i]));
                        if (xCoord == sideLength - 1)
                        {
                            yCoord++;
                            xCoord = 0;
                        }
                        else
                        {
                            xCoord++;
                        }
                    }
                }
                return bitmap;
            }

        }

        public static bool isImageFile()
        {
            string filePath = GlobalVars.FilePath1.ToLower();
            return filePath.EndsWith("bmp") || filePath.EndsWith("jpg") || filePath.EndsWith("png") || filePath.EndsWith("gif");
        }

        public static Bitmap pixelateDrawing(Image image)
        {
            Bitmap bitmap = (Bitmap)image;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color newColor = getNewColor(bitmap.GetPixel(x, y));
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            return bitmap;
        }

        public static Color getNewColor(Color oldColor)
        {
            int grayScale = (int)((oldColor.R * 0.3) + (oldColor.G * 0.59) + (oldColor.B * 0.11));
            //return Color.FromArgb(grayScale, grayScale, grayScale);
            return findNearestColor(oldColor);
        }

        public static Color findNearestColor(Color oldColor)
        {
            int largestDiff = 255 * 3;
            int colorIndex = 0;
            // naive way
            for (int i = 0; i < GlobalVars.mesenColors.Length; i++)
            {
                Color compare = GlobalVars.mesenColors[i];
                int totalDiff = Math.Abs((compare.R - oldColor.R)) + Math.Abs((compare.G - oldColor.G)) + Math.Abs((compare.B - oldColor.B));
                if (totalDiff < largestDiff)
                {
                    largestDiff = totalDiff;
                    colorIndex = i;
                }
            }

            return GlobalVars.mesenColors[colorIndex];
        }
    }
}

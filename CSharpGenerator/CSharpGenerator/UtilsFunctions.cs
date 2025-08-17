// functions in here are for internal use and not actually part of the final program, hence they are uncalled
namespace CSharpGenerator
{
    internal class UtilsFunctions
    {
        public static Bitmap generateApproximateColorSpace()
        {
            Bitmap bitmap = new Bitmap(850, 765);

            int r = 0, g = 0, b = 0;
            for (int z = 0; z < 85; z++)
            {
                int rowNumber = z / 10;
                int colNumber = z % 10;
                int xMin = colNumber * 85;
                int yMin = rowNumber * 85;
                int xMax = xMin + 85;
                int yMax = yMin + 85;
                r = 255 - 3 * z;
                g = 0;
                b = 0;
                for (int y = yMin; y < yMax; y++)
                {
                    g = 0;
                    for (int x = xMin; x < xMax; x++)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                        g += 3;
                    }
                    b += 3;
                }
            }

            // draw some grayscale in the remaining five squares for extra emphasis
            for (int z = 85; z < 90; z++)
            {
                int rowNumber = z / 10;
                int colNumber = z % 10;
                int xMin = colNumber * 85;
                int yMin = rowNumber * 85;
                int xMax = xMin + 85;
                int yMax = yMin + 85;

                for (int y = yMin; y < yMax; y++)
                {
                    r = 255 - 42 * (z - 85);
                    g = r; b = r;
                    for (int x = xMin; x < xMax; x++)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                        r--;
                        g--;
                        b--;
                    }
                }
            }


            GlobalVars.ImageWidth = 850;
            GlobalVars.ImageHeight = 765;
            return bitmap;
        }
    }
}

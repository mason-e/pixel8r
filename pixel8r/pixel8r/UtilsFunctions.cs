// functions in here are for internal use and not actually part of the final program, hence they are uncalled
namespace CSharpGenerator
{
    internal class UtilsFunctions
    {
        public static Bitmap generateApproximateColorSpace()
        {
            const int squareSize = 86;
            const int rows = 11;
            const int cols = 8;
            const int squareCount = rows * cols;
            GlobalVars.ImageWidth = squareSize * rows;
            GlobalVars.ImageHeight = squareSize * cols;
            Bitmap bitmap = new Bitmap(GlobalVars.ImageWidth, GlobalVars.ImageHeight);

            int r = 0, g = 0, b = 0;
            for (int z = 0; z < squareSize; z++)
            {
                int rowNumber = z / rows;
                int colNumber = z % rows;
                int xMin = colNumber * squareSize;
                int yMin = rowNumber * squareSize;
                int xMax = xMin + squareSize;
                int yMax = yMin + squareSize;
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

            // draw some grayscale in the remaining squares for extra emphasis
            if (squareCount > squareSize)
            {
                for (int z = squareSize; z < squareCount; z++)
                {
                    int rowNumber = z / rows;
                    int colNumber = z % rows;
                    int xMin = colNumber * squareSize;
                    int yMin = rowNumber * squareSize;
                    int xMax = xMin + squareSize;
                    int yMax = yMin + squareSize;

                    for (int y = yMin; y < yMax; y++)
                    {
                        // how much each row's starting values can step down to hit 0 by the end of all rows
                        int stepSize = (255 - (squareSize - 1)) / (squareSize - 1);
                        r = 255 - (y - yMin) * stepSize;
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
            }

            return bitmap;
        }
    }
}

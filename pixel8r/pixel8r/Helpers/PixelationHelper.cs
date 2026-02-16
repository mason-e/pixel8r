using SkiaSharp;

namespace pixel8r.Helpers
{
    public class PixelationHelper
    {
        public static SKColor getAverageColor(SKBitmap bitmap, int startX, int startY, int xToEdge, int yToEdge)
        {
            int r = 0, g = 0, b = 0;
            int count = xToEdge * yToEdge;
            for (int y = startY; y < startY + yToEdge; y++)
            {
                for (int x = startX; x < startX + xToEdge; x++)
                {
                    SKColor color = bitmap.GetPixel(x, y);
                    r += color.Red;
                    g += color.Green;
                    b += color.Blue;
                }
            }
            return new SKColor((byte)(r / count), (byte)(g / count), (byte)(b / count));
        }
    }
}
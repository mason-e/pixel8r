using System;
using SkiaSharp;

namespace pixel8r.Helpers
{
    public class ReduceFidelityHelper
    {
        public static SKColor getReducedColor(SKColor color, string selection)
        {
            if (selection == "3 Bit RGB")
            {
                return reduceColor(color, 1);
            }
            if (selection == "6 Bit RGB")
            {
                return reduceColor(color, 2);
            }
            if (selection == "9 Bit RGB")
            {
                return reduceColor(color, 3);
            }
            if (selection == "12 Bit RGB")
            {
                return reduceColor(color, 4);
            }
            if (selection == "15 Bit RGB")
            {
                return reduceColor(color, 5);
            }
            if (selection == "18 Bit RGB")
            {
                return reduceColor(color, 6);
            }
            // default case, should not be reachable
            return color;
        }

        private static SKColor reduceColor(SKColor color, int bits)
        {
            // first reduce down to a rounded multiple of the scale
            int scale = (int)Math.Pow(2, bits) - 1; // subtract 1 because 0 counts as a value
            int r = (int)Math.Round((double)(color.Red * scale) / 255);
            int g = (int)Math.Round((double)(color.Green * scale) / 255);
            int b = (int)Math.Round((double)(color.Blue * scale) / 255);
            // scale back up to 0-255
            r *= 255 / scale;
            g *= 255 / scale;
            b *= 255 / scale;
            return new SKColor((byte)r, (byte)g, (byte)b);
        }
    }
}

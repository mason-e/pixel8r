using System;
using SkiaSharp;

namespace pixel8r.Helpers
{
    public class PaletteProgrammaticHelper
    {
        public static SKColor getProgrammaticColor(SKColor color, string palette)
        {
            if (palette == "Saturate")
            {
                return saturate(color);
            }
            if (palette == "3 Bit RGB")
            {
                return reduceColor(color, 1);
            }
            if (palette == "6 Bit RGB")
            {
                return reduceColor(color, 2);
            }
            if (palette == "9 Bit RGB")
            {
                return reduceColor(color, 3);
            }
            if (palette == "12 Bit RGB")
            {
                return reduceColor(color, 4);
            }
            if (palette == "15 Bit RGB")
            {
                return reduceColor(color, 5);
            }
            if (palette == "18 Bit RGB")
            {
                return reduceColor(color, 6);
            }
            if (palette == "Transpose - RBG")
            {
                return transposeRBG(color);
            }
            if (palette == "Transpose - GRB")
            {
                return transposeGRB(color);
            }
            if (palette == "Transpose - GBR")
            {
                return transposeGBR(color);
            }
            if (palette == "Transpose - BRG")
            {
                return transposeBRG(color);
            }
            if (palette == "Transpose - BGR")
            {
                return transposeBGR(color);
            }

            // default case, should not be reachable
            return color;
        }

        private static SKColor transposeRBG(SKColor color)
        {
            return new SKColor(color.Red, color.Blue, color.Green);
        }

        private static SKColor transposeGRB(SKColor color)
        {
            return new SKColor(color.Green, color.Red, color.Blue);
        }

        private static SKColor transposeGBR(SKColor color)
        {
            return new SKColor(color.Green, color.Blue, color.Red);
        }

        private static SKColor transposeBRG(SKColor color)
        {
            return new SKColor(color.Blue, color.Red, color.Green);
        }

        private static SKColor transposeBGR(SKColor color)
        {
            return new SKColor(color.Blue, color.Green, color.Red);
        }

        private static SKColor saturate(SKColor color)
        {
            float hue, saturation, lightness;
            color.ToHsl(out hue, out saturation, out lightness);
            // SKColor must be converted to a 0-1 scale for S and L - System.Color would've already been on this scale
            saturation /= 100f;
            lightness /= 100f; 
            if (saturation <= 0.95f)
            {
                saturation += 0.05f;
            }
            return ColorConversionHelper.getSaturatedColor(hue, saturation, lightness);
        }

        private static SKColor reduceColor(SKColor color, int bits)
        {
            // first reduce down to a rounded multiple of the scale
            int scale = (int)Math.Pow(2, bits) - 1; // subtract 1 because 0 counts as a value
            int r = (int)Math.Round((double)(color.Red * scale) / 255);
            int g = (int)Math.Round((double)(color.Green * scale) / 255);
            int b = (int)Math.Round((double)(color.Blue * scale) / 255);
            // scale back up to 0-255
            r *= (255 / scale);
            g *= (255 / scale);
            b *= (255 / scale);
            return new SKColor((byte)r, (byte)g, (byte)b);
        }
    }
}

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
            if (palette == "RGB Multiples of 3")
            {
                return findNearestRGBMultiple(color, 3);
            }
            if (palette == "RGB Multiples of 5")
            {
                return findNearestRGBMultiple(color, 5);
            }
            if (palette == "RGB Multiples of 15")
            {
                return findNearestRGBMultiple(color, 15);
            }
            if (palette == "RGB Multiples of 17")
            {
                return findNearestRGBMultiple(color, 17);
            }
            if (palette == "RGB Multiples of 51")
            {
                return findNearestRGBMultiple(color, 51);
            }
            if (palette == "RGB Multiples of 85")
            {
                return findNearestRGBMultiple(color, 85);
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

        private static SKColor findNearestRGBMultiple(SKColor color, int multiple)
        {
            double midPoint = (double)multiple / 2;
            int rModulo = color.Red % multiple;
            int gModulo = color.Green % multiple;
            int bModulo = color.Blue % multiple;
            int newR, newG, newB;

            if (rModulo != 0)
            {
                if (rModulo < midPoint)
                {
                    newR = color.Red - rModulo;
                }
                else
                {
                    newR = color.Red + (multiple - rModulo);
                }
            }
            else
            {
                newR = color.Red;
            }
            if (gModulo != 0)
            {
                if (gModulo < midPoint)
                {
                    newG = color.Green - gModulo;
                }
                else
                {
                    newG = color.Green + (multiple - gModulo);
                }
            }
            else
            {
                newG = color.Green;
            }
            if (bModulo != 0)
            {
                if (bModulo < midPoint)
                {
                    newB = color.Blue - bModulo;
                }
                else
                {
                    newB = color.Blue + (multiple - bModulo);
                }
            }
            else
            {
                newB = color.Blue;
            }
            return new SKColor((byte)newR, (byte)newG, (byte)newB);
        }
    }
}

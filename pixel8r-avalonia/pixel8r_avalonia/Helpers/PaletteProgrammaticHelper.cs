using System.Drawing;

namespace pixel8r_avalonia.Helpers
{
    public class PaletteProgrammaticHelper
    {
        public static Color getProgrammaticColor(Color color, string palette)
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

        private static Color transposeRBG(Color color)
        {
            return Color.FromArgb(color.R, color.B, color.G);
        }

        private static Color transposeGRB(Color color)
        {
            return Color.FromArgb(color.G, color.R, color.B);
        }

        private static Color transposeGBR(Color color)
        {
            return Color.FromArgb(color.G, color.B, color.R);
        }

        private static Color transposeBRG(Color color)
        {
            return Color.FromArgb(color.B, color.R, color.G);
        }

        private static Color transposeBGR(Color color)
        {
            return Color.FromArgb(color.B, color.G, color.R);
        }

        private static Color saturate(Color color)
        {
            float saturation = color.GetSaturation();
            if (saturation <= 0.95f)
            {
                saturation += 0.05f;
            }
            return ColorConversionHelper.getSaturatedColor(color.GetHue(), saturation, color.GetBrightness());
        }

        private static Color findNearestRGBMultiple(Color color, int multiple)
        {
            double midPoint = (double)multiple / 2;
            int rModulo = color.R % multiple;
            int gModulo = color.G % multiple;
            int bModulo = color.B % multiple;
            int newR, newG, newB;

            if (rModulo != 0)
            {
                if (rModulo < midPoint)
                {
                    newR = color.R - rModulo;
                }
                else
                {
                    newR = color.R + (multiple - rModulo);
                }
            }
            else
            {
                newR = color.R;
            }
            if (gModulo != 0)
            {
                if (gModulo < midPoint)
                {
                    newG = color.G - gModulo;
                }
                else
                {
                    newG = color.G + (multiple - gModulo);
                }
            }
            else
            {
                newG = color.G;
            }
            if (bModulo != 0)
            {
                if (bModulo < midPoint)
                {
                    newB = color.B - bModulo;
                }
                else
                {
                    newB = color.B + (multiple - bModulo);
                }
            }
            else
            {
                newB = color.B;
            }
            return Color.FromArgb(newR, newG, newB);
        }
    }
}

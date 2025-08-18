namespace CSharpGenerator
{
    internal class PaletteProgrammaticFunctions
    {
        public static Color getProgrammaticColor(Color color, string palette)
        {
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

        private static Color findNearestRGBMultiple(Color oldColor, int multiple)
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
    }
}

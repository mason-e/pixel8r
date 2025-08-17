namespace CSharpGenerator
{
    internal class PaletteProgrammaticFunctions
    {
        public static Color getProgrammaticColor(Color oldColor, string palette)
        {
            if (palette == "Grayscale")
            {
                return grayScale(oldColor);
            }
            if (palette == "RGB Multiples of 3")
            {
                return findNearestRGBMultiple(oldColor, 3);
            }
            if (palette == "RGB Multiples of 5")
            {
                return findNearestRGBMultiple(oldColor, 5);
            }
            if (palette == "RGB Multiples of 15")
            {
                return findNearestRGBMultiple(oldColor, 15);
            }
            if (palette == "RGB Multiples of 17")
            {
                return findNearestRGBMultiple(oldColor, 17);
            }
            if (palette == "RGB Multiples of 51")
            {
                return findNearestRGBMultiple(oldColor, 51);
            }
            if (palette == "RGB Multiples of 85")
            {
                return findNearestRGBMultiple(oldColor, 85);
            }
            // default case, should not be reachable
            return oldColor;
        }

        private static Color grayScale(Color oldColor)
        {
            int grayScale = (int)((oldColor.R * 0.3) + (oldColor.G * 0.59) + (oldColor.B * 0.11));
            return Color.FromArgb(grayScale, grayScale, grayScale);
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

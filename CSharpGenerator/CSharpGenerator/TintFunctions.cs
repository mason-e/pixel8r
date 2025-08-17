namespace CSharpGenerator
{
    internal class TintFunctions
    {
        // for now make the value universal to each function for easy experimentation, but not user-selectable
        private const int tintDelta = 10;

        public static Color getTintColor(Color oldColor, string tint)
        {
            if (tint == "White (Brighten)")
            {
                return tintWhite(oldColor);
            }
            if (tint == "Red (Soft)")
            {
                return tintRedSoft(oldColor);
            }
            if (tint == "Red (Hard)")
            {
                return tintRedHard(oldColor);
            }
            if (tint == "Green (Soft)")
            {
                return tintGreenSoft(oldColor);
            }
            if (tint == "Green (Hard)")
            {
                return tintGreenHard(oldColor);
            }
            if (tint == "Blue (Soft)")
            {
                return tintBlueSoft(oldColor);
            }
            if (tint == "Blue (Hard)")
            {
                return tintBlueHard(oldColor);
            }
            if (tint == "Cyan (Soft)")
            {
                return tintCyanSoft(oldColor);
            }
            if (tint == "Cyan (Hard)")
            {
                return tintCyanHard(oldColor);
            }
            if (tint == "Magenta (Soft)")
            {
                return tintMagentaSoft(oldColor);
            }
            if (tint == "Magenta (Hard)")
            {
                return tintMagentaHard(oldColor);
            }
            if (tint == "Yellow (Soft)")
            {
                return tintYellowSoft(oldColor);
            }
            if (tint == "Yellow (Hard)")
            {
                return tintYellowHard(oldColor);
            }
            if (tint == "Black (Darken)")
            {
                return tintBlack(oldColor);
            }
            // default case, should not be reachable
            return oldColor;
        }

        private static Color tintWhite(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintRedSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), originalColor.G, originalColor.B);
        }

        private static Color tintRedHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), subtractTintValue(originalColor.B, tintDelta));
        }

        private static Color tintGreenSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, addTintValue(originalColor.G, tintDelta), originalColor.B);
        }

        private static Color tintGreenHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, 10), subtractTintValue(originalColor.B, tintDelta));
        }

        private static Color tintBlueSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, originalColor.G, addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintBlueHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintCyanSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintCyanHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintMagentaSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), originalColor.G, addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintMagentaHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        private static Color tintYellowSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), originalColor.B);
        }

        private static Color tintYellowHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), subtractTintValue(originalColor.B, tintDelta));
        }

        private static Color tintBlack(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), subtractTintValue(originalColor.B, tintDelta));
        }

        private static int addTintValue(int originalValue, int addition)
        {
            return originalValue <= 255 - addition ? originalValue + addition : 255;
        }

        private static int subtractTintValue(int originalValue, int subtractition)
        {
            return originalValue >= subtractition ? originalValue - subtractition : 0;
        }
    }
}

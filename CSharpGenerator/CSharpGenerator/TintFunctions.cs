namespace CSharpGenerator
{
    internal class TintFunctions
    {
        // for now make the value universal to each function for easy experimentation, but not user-selectable
        private const int tintDelta = 10;

        public static Color tintWhite(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintRedSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), originalColor.G, originalColor.B);
        }

        public static Color tintRedHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), subtractTintValue(originalColor.B, tintDelta));
        }

        public static Color tintGreenSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, addTintValue(originalColor.G, tintDelta), originalColor.B);
        }

        public static Color tintGreenHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, 10), subtractTintValue(originalColor.B, tintDelta));
        }

        public static Color tintBlueSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, originalColor.G, addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintBlueHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintCyanSoft(Color originalColor)
        {
            return Color.FromArgb(originalColor.R, addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintCyanHard(Color originalColor)
        {
            return Color.FromArgb(subtractTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintMagentaSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), originalColor.G, addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintMagentaHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), subtractTintValue(originalColor.G, tintDelta), addTintValue(originalColor.B, tintDelta));
        }

        public static Color tintYellowSoft(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), originalColor.B);
        }

        public static Color tintYellowHard(Color originalColor)
        {
            return Color.FromArgb(addTintValue(originalColor.R, tintDelta), addTintValue(originalColor.G, tintDelta), subtractTintValue(originalColor.B, tintDelta));
        }

        public static Color tintBlack(Color originalColor)
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

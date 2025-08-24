namespace CSharpGenerator
{
    internal class TintFunctions
    {
        // for now make the value universal to each function for easy experimentation, but not user-selectable
        private const int tintDelta = 10;

        public static Color getTintColor(Color color, string tint)
        {
            if (tint.Contains("scale"))
            {
                return colorScale(color, tint);
            }
            if (tint == "White (Brighten)")
            {
                return tintWhite(color);
            }
            if (tint == "Black (Darken)")
            {
                return tintBlack(color);
            }
            if (tint == "Red (Soft)")
            {
                return tintRedSoft(color);
            }
            if (tint == "Red (Hard)")
            {
                return tintRedHard(color);
            }
            if (tint == "Green (Soft)")
            {
                return tintGreenSoft(color);
            }
            if (tint == "Green (Hard)")
            {
                return tintGreenHard(color);
            }
            if (tint == "Blue (Soft)")
            {
                return tintBlueSoft(color);
            }
            if (tint == "Blue (Hard)")
            {
                return tintBlueHard(color);
            }
            if (tint == "Cyan (Soft)")
            {
                return tintCyanSoft(color);
            }
            if (tint == "Cyan (Hard)")
            {
                return tintCyanHard(color);
            }
            if (tint == "Magenta (Soft)")
            {
                return tintMagentaSoft(color);
            }
            if (tint == "Magenta (Hard)")
            {
                return tintMagentaHard(color);
            }
            if (tint == "Yellow (Soft)")
            {
                return tintYellowSoft(color);
            }
            if (tint == "Yellow (Hard)")
            {
                return tintYellowHard(color);
            }
            // default case, should not be reachable
            return color;
        }

        private static Color colorScale(Color color, string tint)
        {
            int average = (int)((color.R + color.G + color.B) / 3);
            // default values correspond to grayscale
            double weightR = 1.0;
            double weightG = 1.0;
            double weightB = 1.0;

            // values vary due to different perceptual strengths of each primary
            if (tint == "Redscale")
            {
                weightR = 0.9;
                weightG = 0.05;
                weightB = 0.05;
            }
            if (tint == "Greenscale") 
            {
                weightR = 0.1;
                weightG = 0.8;
                weightB = 0.1;
            }
            if (tint == "Bluescale")
            {
                weightR = 0.15;
                weightG = 0.3;
                weightB = 0.55;
            }
            if (tint == "Cyanscale")
            {
                weightR = 0.1;
                weightG = 0.45;
                weightB = 0.45;
            }
            if (tint == "Magentascale")
            {
                weightR = 0.6;
                weightG = 0.1;
                weightB = 0.3;
            }
            if (tint == "Yellowscale")
            {
                weightR = 0.55;
                weightG = 0.4;
                weightB = 0.05;
            }

            return Color.FromArgb((int)(average * weightR), (int)(average * weightG), (int)(average * weightB));
        }

        private static Color tintWhite(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), addTint(color.G, tintDelta), addTint(color.B, tintDelta));
        }

        private static Color tintBlack(Color color)
        {
            return Color.FromArgb(subtractTint(color.R, tintDelta), subtractTint(color.G, tintDelta), subtractTint(color.B, tintDelta));
        }

        private static Color tintRedSoft(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), color.G, color.B);
        }

        private static Color tintRedHard(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), subtractTint(color.G, tintDelta), subtractTint(color.B, tintDelta));
        }

        private static Color tintGreenSoft(Color color)
        {
            return Color.FromArgb(color.R, addTint(color.G, tintDelta), color.B);
        }

        private static Color tintGreenHard(Color color)
        {
            return Color.FromArgb(subtractTint(color.R, tintDelta), addTint(color.G, 10), subtractTint(color.B, tintDelta));
        }

        private static Color tintBlueSoft(Color color)
        {
            return Color.FromArgb(color.R, color.G, addTint(color.B, tintDelta));
        }

        private static Color tintBlueHard(Color color)
        {
            return Color.FromArgb(subtractTint(color.R, tintDelta), subtractTint(color.G, tintDelta), addTint(color.B, tintDelta));
        }

        private static Color tintCyanSoft(Color color)
        {
            return Color.FromArgb(color.R, addTint(color.G, tintDelta), addTint(color.B, tintDelta));
        }

        private static Color tintCyanHard(Color color)
        {
            return Color.FromArgb(subtractTint(color.R, tintDelta), addTint(color.G, tintDelta), addTint(color.B, tintDelta));
        }

        private static Color tintMagentaSoft(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), color.G, addTint(color.B, tintDelta));
        }

        private static Color tintMagentaHard(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), subtractTint(color.G, tintDelta), addTint(color.B, tintDelta));
        }

        private static Color tintYellowSoft(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), addTint(color.G, tintDelta), color.B);
        }

        private static Color tintYellowHard(Color color)
        {
            return Color.FromArgb(addTint(color.R, tintDelta), addTint(color.G, tintDelta), subtractTint(color.B, tintDelta));
        }

        private static int addTint(int original, int value)
        {
            return original <= 255 - value ? original + value : 255;
        }

        private static int subtractTint(int original, int value)
        {
            return original >= value ? original - value : 0;
        }
    }
}

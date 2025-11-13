using SkiaSharp;

namespace pixel8r.Helpers
{
    public class TintHelper
    {
        // for now make the value universal to each function for easy experimentation, but not user-selectable
        private const int tintDelta = 10;

        public static SKColor getTintColor(SKColor color, string tint)
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

        private static SKColor colorScale(SKColor color, string tint)
        {
            int average = (int)((color.Red + color.Green + color.Blue) / 3);
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

            return new SKColor((byte)(average * weightR), (byte)(average * weightG), (byte)(average * weightB));
        }

        private static SKColor tintWhite(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), addTint(color.Green, tintDelta), addTint(color.Blue, tintDelta));
        }

        private static SKColor tintBlack(SKColor color)
        {
            return new SKColor(subtractTint(color.Red, tintDelta), subtractTint(color.Green, tintDelta), subtractTint(color.Blue, tintDelta));
        }

        private static SKColor tintRedSoft(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), color.Green, color.Blue);
        }

        private static SKColor tintRedHard(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), subtractTint(color.Green, tintDelta), subtractTint(color.Blue, tintDelta));
        }

        private static SKColor tintGreenSoft(SKColor color)
        {
            return new SKColor(color.Red, addTint(color.Green, tintDelta), color.Blue);
        }

        private static SKColor tintGreenHard(SKColor color)
        {
            return new SKColor(subtractTint(color.Red, tintDelta), addTint(color.Green, 10), subtractTint(color.Blue, tintDelta));
        }

        private static SKColor tintBlueSoft(SKColor color)
        {
            return new SKColor(color.Red, color.Green, addTint(color.Blue, tintDelta));
        }

        private static SKColor tintBlueHard(SKColor color)
        {
            return new SKColor(subtractTint(color.Red, tintDelta), subtractTint(color.Green, tintDelta), addTint(color.Blue, tintDelta));
        }

        private static SKColor tintCyanSoft(SKColor color)
        {
            return new SKColor(color.Red, addTint(color.Green, tintDelta), addTint(color.Blue, tintDelta));
        }

        private static SKColor tintCyanHard(SKColor color)
        {
            return new SKColor(subtractTint(color.Red, tintDelta), addTint(color.Green, tintDelta), addTint(color.Blue, tintDelta));
        }

        private static SKColor tintMagentaSoft(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), color.Green, addTint(color.Blue, tintDelta));
        }

        private static SKColor tintMagentaHard(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), subtractTint(color.Green, tintDelta), addTint(color.Blue, tintDelta));
        }

        private static SKColor tintYellowSoft(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), addTint(color.Green, tintDelta), color.Blue);
        }

        private static SKColor tintYellowHard(SKColor color)
        {
            return new SKColor(addTint(color.Red, tintDelta), addTint(color.Green, tintDelta), subtractTint(color.Blue, tintDelta));
        }

        private static byte addTint(byte original, int value)
        {
            return (byte)(original <= 255 - value ? original + value : 255);
        }

        private static byte subtractTint(byte original, int value)
        {
            return (byte)(original >= value ? original - value : 0);
        }
    }
}

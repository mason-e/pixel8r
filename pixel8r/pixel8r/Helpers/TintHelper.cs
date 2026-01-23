using SkiaSharp;

namespace pixel8r.Helpers
{
    public class TintHelper
    {
        public static SKColor getTintScaleColor(SKColor color, string selection)
        {
            return colorScale(color, selection);
        }

        public static SKColor getTintColor(SKColor color, string tint, int value, bool isHard)
        {
            if (tint == "Black < -- > White")
            {
                return tintBlackWhite(color, value);
            }
            if (tint == "Cyan < -- > Red")
            {
                return tintCyanRed(color, value, isHard);
            }
            if (tint == "Magenta < -- > Green")
            {
                return tintMagentaGreen(color, value, isHard);
            }
            if (tint == "Yellow < -- > Blue")
            {
                return tintYellowBlue(color, value, isHard);
            }
            // default case, should not be reachable
            return color;
        }

        private static SKColor colorScale(SKColor color, string selection)
        {
            int average = (int)((color.Red + color.Green + color.Blue) / 3);
            // default values correspond to grayscale
            double weightR = 1.0;
            double weightG = 1.0;
            double weightB = 1.0;

            // values vary due to different perceptual strengths of each primary
            if (selection == "Redscale")
            {
                weightR = 0.9;
                weightG = 0.05;
                weightB = 0.05;
            }
            if (selection == "Greenscale")
            {
                weightR = 0.1;
                weightG = 0.8;
                weightB = 0.1;
            }
            if (selection == "Bluescale")
            {
                weightR = 0.15;
                weightG = 0.3;
                weightB = 0.55;
            }
            if (selection == "Cyanscale")
            {
                weightR = 0.1;
                weightG = 0.45;
                weightB = 0.45;
            }
            if (selection == "Magentascale")
            {
                weightR = 0.6;
                weightG = 0.1;
                weightB = 0.3;
            }
            if (selection == "Yellowscale")
            {
                weightR = 0.55;
                weightG = 0.4;
                weightB = 0.05;
            }

            return new SKColor((byte)(average * weightR), (byte)(average * weightG), (byte)(average * weightB));
        }

        private static SKColor tintBlackWhite(SKColor color, int value)
        {
            return new SKColor(addTint(color.Red, value), addTint(color.Green, value), addTint(color.Blue, value));
        }

        private static SKColor tintCyanRed(SKColor color, int value, bool isHard)
        {
            if (isHard)
            {
                return new SKColor(addTint(color.Red, value), addTint(color.Green, -value), addTint(color.Blue, -value));
            }
            else
            {
                return new SKColor(addTint(color.Red, value), color.Green, color.Blue);
            }
        }

        private static SKColor tintMagentaGreen(SKColor color, int value, bool isHard)
        {
            if (isHard)
            {
                return new SKColor(addTint(color.Red, -value), addTint(color.Green, value), addTint(color.Blue, -value));
            }
            else
            {
                return new SKColor(color.Red, addTint(color.Green, value), color.Blue);
            }
        }

        private static SKColor tintYellowBlue(SKColor color, int value, bool isHard)
        {
            if (isHard)
            {
                return new SKColor(addTint(color.Red, -value), addTint(color.Green, -value), addTint(color.Blue, value));
            }
            else
            {
                return new SKColor(color.Red, color.Green, addTint(color.Blue, value));
            }
        }

        private static byte addTint(byte original, int value)
        {
            int tinted = original + value;
            return (byte)(tinted < 255 ? (tinted > 0 ? tinted : 0) : 255);
        }
    }
}

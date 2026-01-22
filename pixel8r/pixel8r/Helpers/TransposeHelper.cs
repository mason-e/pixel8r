using SkiaSharp;

namespace pixel8r.Helpers
{
    public class TransposeHelper
    {
        public static SKColor getTransposedColor(SKColor color, string selection)
        {
            if (selection == "RBG")
            {
                return transposeRBG(color);
            }
            if (selection == "GRB")
            {
                return transposeGRB(color);
            }
            if (selection == "GBR")
            {
                return transposeGBR(color);
            }
            if (selection == "BRG")
            {
                return transposeBRG(color);
            }
            if (selection == "BGR")
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
    }
}

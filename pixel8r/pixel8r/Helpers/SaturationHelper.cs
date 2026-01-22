using SkiaSharp;

namespace pixel8r.Helpers
{
    public class SaturationHelper
    {
        public static SKColor getSaturatedColor(SKColor color, string selection)
        {
            if (selection == "Saturate")
            {
                return saturate(color);
            }

            // default case, should not be reachable
            return color;
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
    }
}

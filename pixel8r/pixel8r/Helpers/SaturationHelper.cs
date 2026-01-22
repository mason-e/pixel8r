using SkiaSharp;

namespace pixel8r.Helpers
{
    public class SaturationHelper
    {
        public static SKColor getSaturatedColor(SKColor color, int percent)
        {
            return saturate(color, percent);
        }

        private static SKColor saturate(SKColor color, int percent)
        {
            float hue, saturation, lightness;
            color.ToHsl(out hue, out saturation, out lightness);
            float change = percent / 100f;
            // SKColor must be converted to a 0-1 scale for S and L - System.Color would've already been on this scale
            saturation /= 100f;
            lightness /= 100f; 
            if (saturation + change >= 1.0f)
            {
                saturation = 1.0f;
            }
            else if (saturation + change <= 0.0f)
            {
                saturation = 0.0f;
            }
            else
            {
                saturation += change;
            }
            return ColorConversionHelper.getSaturatedColor(hue, saturation, lightness);
        }
    }
}

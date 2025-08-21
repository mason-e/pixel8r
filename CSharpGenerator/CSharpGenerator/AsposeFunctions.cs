// this was mostly kept separate to minimize the amount of Aspose vs System "Color" naming conflicts
using Aspose.Svg.Drawing;
using Color = Aspose.Svg.Drawing.Color;

namespace CSharpGenerator
{
    internal class AsposeFunctions
    {
        public static (double, double, double) getLabColor(int r, int g, int b)
        {
            Color convertedRGB = Color.FromRgb(r, g, b);
            IColorComponents convertedLAB = convertedRGB.Convert(ColorModel.Lab);
            return (convertedLAB.Components[0], convertedLAB.Components[1], convertedLAB.Components[2]);
        }

        public static System.Drawing.Color getSaturatedColor(float h, float s, float l)
        {
            Color saturated = Color.FromHsl(h, s, l);
            string rgb = saturated.ToRgbHexString();
            int r = Convert.ToInt32(rgb.Substring(1, 2), 16);
            int g = Convert.ToInt32(rgb.Substring(3, 2), 16);
            int b = Convert.ToInt32(rgb.Substring(5, 2), 16);
            return System.Drawing.Color.FromArgb(r, g, b);
        }
    }
}

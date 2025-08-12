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
    }
}

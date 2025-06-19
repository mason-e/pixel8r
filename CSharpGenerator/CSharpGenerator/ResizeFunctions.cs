using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGenerator
{
    internal class ResizeFunctions
    {
        public static (int, int) getCropDimensions (int desiredWidth, int desiredHeight)
        {
            double currentAspectRatio = (double) GlobalVars.ImageSizeX / GlobalVars.ImageSizeY;
            double desiredAspectRatio = (double) desiredWidth / desiredHeight;
            // image too tall - keep the width dimension
            if (desiredAspectRatio > currentAspectRatio)
            {
                int newHeight = (int)(desiredHeight * (double)GlobalVars.ImageSizeX / desiredWidth);
                return (GlobalVars.ImageSizeX, newHeight);
            }
            // image too wide - keep the height dimension
            else
            {
                int newWidth = (int)(desiredWidth * (double)GlobalVars.ImageSizeY / desiredHeight);
                return (newWidth, GlobalVars.ImageSizeY);
            }
        }
    }
}

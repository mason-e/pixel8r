using System;

namespace pixel8r.Helpers
{
    public class ResizeHelper
    {
        public static (int, int) getCropDimensions(int desiredWidth, int desiredHeight)
        {
            double currentAspectRatio = (double)GlobalVars.ImageWidth / GlobalVars.ImageHeight;
            double desiredAspectRatio = (double)desiredWidth / desiredHeight;
            // image too tall - keep the width dimension
            if (desiredAspectRatio > currentAspectRatio)
            {
                int newHeight = (int)(desiredHeight * (double)GlobalVars.ImageWidth / desiredWidth);
                return (GlobalVars.ImageWidth, newHeight);
            }
            // image too wide - keep the height dimension
            else
            {
                int newWidth = (int)(desiredWidth * (double)GlobalVars.ImageHeight / desiredHeight);
                return (newWidth, GlobalVars.ImageHeight);
            }
        }

        public static (int, int) getResizeDimensions(int desiredPercent)
        {
            double multiplier = (double)desiredPercent / 100;
            return ((int)(multiplier * GlobalVars.ImageWidth), (int)(multiplier * GlobalVars.ImageHeight));
        }

        public static (int, int) getResizeBounds()
        {
            int minX = 20;
            int minY = 20;
            int maxX = 1280;
            int maxY = 720;

            // larger value of percent needed to reduce either dimension is the minimum percent
            int reductionToMin = (int)(100 * Math.Max((float)minX / GlobalVars.ImageWidth, (float)minY / GlobalVars.ImageHeight));
            // smaller value of percent needed to expand either dimension is the maximum percent
            int expansionToMax = (int)(100 * Math.Min((float)maxX / GlobalVars.ImageWidth, (float)maxY / GlobalVars.ImageHeight));
            return (reductionToMin, expansionToMax);
        }
    }
}

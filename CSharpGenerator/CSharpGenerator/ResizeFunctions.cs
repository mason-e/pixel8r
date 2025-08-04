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

        public static (int, int) getResizeDimensions (int desiredPercent)
        {
            double multiplier = (double) desiredPercent / 100;
            return ((int) (multiplier * GlobalVars.ImageSizeX), (int) (multiplier * GlobalVars.ImageSizeY));
        }

        public static (int, int) getResizeBounds()
        {
            int minX = 20;
            int minY = 20;
            int maxX = 1118;
            int maxY = 720;

            // larger value of percent needed to reduce either dimension is the minimum percent
            int reductionToMin = (int)(100 * Math.Max((float) minX / GlobalVars.ImageSizeX, (float) minY / GlobalVars.ImageSizeY));
            // smaller value of percent needed to expand either dimension is the maximum percent
            int expansionToMax = (int)(100 * Math.Max((float )(GlobalVars.ImageSizeX / maxX), (float) maxY / GlobalVars.ImageSizeY));
            return (reductionToMin, expansionToMax);
        }

        public static void drawCenterPointRectangle (Graphics graphics, int width, int height, int centerX, int centerY)
        {
            // recntangles are drawn from top left corner
            int startX = centerX - width / 2;
            int startY = centerY - height / 2;
            Rectangle cursor = new Rectangle(startX, startY, width, height);
            graphics.DrawRectangle(new Pen(Color.LightBlue, 2), cursor);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 255, 255, 255)), cursor);
        }

        public static Bitmap cropBitmap(Bitmap original, int xStart, int yStart, int resizeWidth, int resizeHeight)
        {
            // account for it displaying in center of image area
            int actualXStart = xStart - (1118 - GlobalVars.ImageSizeX) / 2;
            int actualYStart = yStart - (720 - GlobalVars.ImageSizeY) / 2;
            Rectangle cropArea = new Rectangle(xStart, yStart, resizeWidth, resizeHeight);
            Bitmap newImage = original.Clone(cropArea, original.PixelFormat);
            GlobalVars.ImageSizeX = newImage.Width;
            GlobalVars.ImageSizeY = newImage.Height;
            return newImage;
        }
    }
}

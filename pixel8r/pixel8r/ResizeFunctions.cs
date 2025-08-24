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
            double currentAspectRatio = (double) GlobalVars.ImageWidth / GlobalVars.ImageHeight;
            double desiredAspectRatio = (double) desiredWidth / desiredHeight;
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

        public static (int, int) getResizeDimensions (int desiredPercent)
        {
            double multiplier = (double) desiredPercent / 100;
            return ((int) (multiplier * GlobalVars.ImageWidth), (int) (multiplier * GlobalVars.ImageHeight));
        }

        public static (int, int) getResizeBounds()
        {
            int minX = 20;
            int minY = 20;
            int maxX = 1118;
            int maxY = 720;

            // larger value of percent needed to reduce either dimension is the minimum percent
            int reductionToMin = (int)(100 * Math.Max((float) minX / GlobalVars.ImageWidth, (float) minY / GlobalVars.ImageHeight));
            // smaller value of percent needed to expand either dimension is the maximum percent
            int expansionToMax = (int)(100 * Math.Min((float) maxX / GlobalVars.ImageWidth, (float) maxY / GlobalVars.ImageHeight));
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
            int actualXStart = xStart - (1118 - GlobalVars.ImageWidth) / 2;
            int actualYStart = yStart - (720 - GlobalVars.ImageHeight) / 2;
            Rectangle cropArea = new Rectangle(xStart, yStart, resizeWidth, resizeHeight);
            Bitmap newImage = original.Clone(cropArea, original.PixelFormat);
            GlobalVars.ImageWidth = newImage.Width;
            GlobalVars.ImageHeight = newImage.Height;
            return newImage;
        }
    }
}

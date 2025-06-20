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

        public static void drawCenterPointRectangle (Graphics graphics, int width, int height)
        {
            int centerX = 559;
            int centerY = 360;
            // recntangles are drawn from top left corner
            int startX = centerX - width / 2;
            int startY = centerY - height / 2;
            Rectangle cursor = new Rectangle(startX, startY, width, height);
            graphics.DrawRectangle(new Pen(Color.LightBlue, 2), cursor);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 255, 255, 255)), cursor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace Mosaicus
{
    public class PictureTools
    {
        //
        // Структура для среднего цвета
        //
        public struct avColor
        {
            public int R;
            public int G;
            public int B;
        }

        //
        // Вычисление среднего цвета изображения
        //
        public static avColor getAvColor(Bitmap bmp)
        {
            avColor av = new avColor();

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color c = bmp.GetPixel(j, i);
                    av.R += c.R;
                    av.G += c.G;
                    av.B += c.B;
                }
            }

            av.R /= bmp.Height * bmp.Width;
            av.G /= bmp.Height * bmp.Width;
            av.B /= bmp.Height * bmp.Width;

            bmp.Dispose();

            return av;
        }

        //
        // Получение расстояния между цветами
        //
        public static double getDist(Color c1, Color c2)
        {
            // Эвклидова мера
            return Math.Sqrt(Math.Pow(c2.R - c1.R, 2) + Math.Pow(c2.G - c1.G, 2) + Math.Pow(c2.B - c1.B, 2));
        }

    }
}
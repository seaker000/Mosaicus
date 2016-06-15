using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Mosaicus.Models
{
    public class Mosaic
    {

        public static string processing(string picPath, string picture, int picSize)
        {
            string picFile = "pics.txt";
            string result = "";
            
            Bitmap bmp = new Bitmap(picPath + "/UsersPhoto/" + picture);

            for (int i = 0; i < bmp.Height - picSize; i += picSize)
            {
                for (int j = 0; j < bmp.Width - picSize; j += picSize)
                {
                    // Определяем средний цвет окна подопытного изображения
                    PictureTools.avColor avCt = new PictureTools.avColor();

                    for (int k = 0; k < picSize; k++)
                    {
                        for (int q = 0; q < picSize; q++)
                        {
                            Color c = bmp.GetPixel(j + k, i + q); // Читаем каждый пиксель подопытной картинки
                            avCt.R += c.R;
                            avCt.G += c.G;
                            avCt.B += c.B;
                        }
                    }

                    avCt.R /= picSize * picSize;
                    avCt.G /= picSize * picSize;
                    avCt.B /= picSize * picSize;

                    Color CMain = Color.FromArgb(avCt.R, avCt.G, avCt.B);

                    // Прогоняем по всей базе изображений полученный средний цвет
                    // и считаем расстояние между цветами (Эвклидова мера)

                    Dictionary<string, double> distList = new Dictionary<string, double>();

                    foreach (string str in System.IO.File.ReadAllLines(picPath + "/" + picFile))
                    {
                        string[] SColor = str.Split(';')[1].Split(',');

                        Color ct = Color.FromArgb(Int32.Parse(SColor[0]), Int32.Parse(SColor[1]), Int32.Parse(SColor[2]));

                        distList.Add(str.Split(';')[0], PictureTools.getDist(ct, CMain)); // Считаем расстояние

                    }

                    string winner = "";

                    foreach (var item in distList)
                    {
                        if (item.Value.Equals(distList.Values.Min()))
                            winner = item.Key;
                    }

                    result += String.Format("<img src='/Home/Data/{0}' />", winner);
                }

                result += "<br>";
            }

            bmp.Dispose();

            return result;
        }

    }
}
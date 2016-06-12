using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlickrNet;
using System.Net;
using System.IO;
using System.Drawing;
using System.Web.Mvc;

namespace Mosaicus.Models
{
    public class PictureDB
    {

        //
        // Получение базы изображений
        //
        public static string getPrictures(string picPath, int picSize)
        {
            string picFile = "pics.txt";
            string result = "";

            Flickr flickr = new Flickr("a03ec58d1b94489ebdd0ac6372942ec6");

            var options = new PhotoSearchOptions { Tags = "colorful", PerPage = 500, Page = 1 };
            PhotoCollection photos = flickr.PhotosSearch(options);

            WebClient client = new WebClient();

            foreach (Photo photo in photos)
            {
                PictureTools.avColor av;
                client.DownloadFile(photo.SquareThumbnailUrl, picPath + "/" + photo.SquareThumbnailUrl.Split('/').Last());

                using (Image img = Image.FromFile(picPath + "/" + photo.SquareThumbnailUrl.Split('/').Last()))
                {
                    Bitmap bmp = new Bitmap(img, picSize, picSize);
                    img.Dispose();
                    bmp.Save(picPath + "/" + photo.SquareThumbnailUrl.Split('/').Last());
                    av = PictureTools.getAvColor((Bitmap)bmp.Clone());
                    bmp.Dispose();
                }

                result += String.Format("<img src='/Home/Data/{0}' width='30px'/> <div style='display: inline-flex; width:30px; height: 30px; background-color: rgb({1}, {2}, {3})'></div><br>", photo.SquareThumbnailUrl.Split('/').Last(), av.R, av.G, av.B);

                using (StreamWriter sw = System.IO.File.AppendText(picPath + "/" + picFile))
                {
                    sw.WriteLine(photo.SquareThumbnailUrl.Split('/').Last() + ";" + av.R + "," + av.G + "," + av.B);
                }
            }

            return result;
        }

        

    }
}
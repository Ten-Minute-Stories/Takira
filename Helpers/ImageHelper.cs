using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Svg;

namespace Takira.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource ConvertImage(string base64, int width, int height)
        {
            byte[] binaryData = Convert.FromBase64String(base64);

            using (MemoryStream svgStream = new MemoryStream(binaryData))
            {
                SvgDocument svgDocument = SvgDocument.Open<SvgDocument>(svgStream);
                svgDocument.Height = height;
                svgDocument.Width = width;
                Bitmap bitmap = svgDocument.Draw();
                using (MemoryStream biStreamSource = new MemoryStream())
                {
                    bitmap.Save(biStreamSource, ImageFormat.Png);
                    biStreamSource.Position = 0;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = biStreamSource;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                    return bi;
                }
            }
        }
    }
}
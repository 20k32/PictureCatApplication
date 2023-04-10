using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PictureCat.HelpClassesForGeneralUse
{
    public static class ImageExtensions
    {
        public static void SetImageSourceOnLoad(this Image currentImage, string path)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path, UriKind.Relative);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            currentImage.Source = image;
        }

        public static void SetImageSourceOnLoadFromByteArray(this Image currentImage, byte[] imageBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memoryStream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                currentImage.Source = image;
            }
        }

        public static void SetImageSourceFromResources(this Image currentImage ,string path)
        {
            currentImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace PictureCat
{
    public static class LoadingProgress
    {
        private static Uri LoadingUri;
        private static BitmapImage IconImage;

        static LoadingProgress()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("Images/Logo.png", UriKind.Relative);
            image.EndInit();
            IconImage = image;
            LoadingUri = new Uri("Images/LoadingGear.gif", UriKind.Relative);
        }

        public static void InitializeLoading(Image imageToLoadGif)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = LoadingUri;
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imageToLoadGif, image);
        }
        public static void FinishLoading(Image imageToLoadIcon)
        {
            ImageBehavior.GetAnimationController(imageToLoadIcon).Dispose();
            imageToLoadIcon.Source = IconImage;
        }
    }
}

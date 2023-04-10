using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PictureCat
{
    /// <summary>
    /// Логика взаимодействия для FullSizeImage.xaml
    /// </summary>
    public partial class FullSizeImage : Window
    {
        public FullSizeImage(ImageSource bitmapImage)
        {
            InitializeComponent();
            Image.Source = bitmapImage;
            Image.Stretch = Stretch.Uniform;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}

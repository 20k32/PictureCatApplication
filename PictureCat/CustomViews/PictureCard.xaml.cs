using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PictureCat
{
    /// <summary>
    /// Логика взаимодействия для PictureCard.xaml
    /// </summary>
    /// 

    public partial class PictureCard : UserControl
    {
        public ImageCardInformation Information { get; set; } = null!;
        public PictureCard()
        {
            InitializeComponent();
        }

        public PictureCard Clone(ImageCardInformation information)
        {
            return new PictureCard() { Information = information.Clone() };
        }

        private void CurrentPicture_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Information.CopyImageToClipBoard();
            BorderAnimation.DoReverseAnimation(Completed!, ImageBorder);
        }
        private void Completed(object sender, EventArgs e)
        {
            ImageBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 225, 215, 198));
        }
    }
}

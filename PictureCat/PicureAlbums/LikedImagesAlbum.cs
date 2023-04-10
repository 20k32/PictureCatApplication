using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PictureCat
{
    public class LikedImagesAlbum : Album
    {
        public LikedImagesAlbum(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Image MainPageImage) : base(flexWrapPanel, ownerWindow, Visibility.Collapsed, Visibility.Visible, string.Empty, MainPageImage)
        { }

        public override async Task LoadImageCardsAsync()
        {
            Application.Current.Dispatcher.Invoke(() => MainPageScrollViewer?.ScrollToHome());
            AddedUserImagesAlbum currentCategoryAlbum = new AddedUserImagesAlbum(CurrentPanel, OwnerWindow, MainPageImage);
            currentCategoryAlbum.SetControlosToBlock(controlosToBlock);
            currentCategoryAlbum.SetImagesDynamically(Images);
            await currentCategoryAlbum.LoadImageCardsAsync();
            GC.Collect();
        }

        public override void SetImages()
        {
            Images = ApplicationDbContext.GetInstance()
                .Images
                .Where(i => i.Liked == true)
                .Select(i => i.Path)
                .ToArray();
        }
    }
}

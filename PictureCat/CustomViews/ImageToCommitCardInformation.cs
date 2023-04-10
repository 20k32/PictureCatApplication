using PictureCat.HelpClassesForGeneralUse;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PictureCat.CustomViews
{
    public class ImageToCommitCardInformation : ImageCardInformation
    {
        public ImageToCommitCardInformation()
        {
            SaveChangesLocal = new MyRoutedCommand(SaveChangesLocalExecute, SaveChangesLocalCanExecute);
        }
        public override void AddToDbCommandExecute(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public override ImageCardInformation Clone()
        {
            return new ImageToCommitCardInformation();
        }

        public override void CopyImageToClipBoard()
        {
            Helper.CopyImageToClipBoardFromByteArray(Path, CurentImageBytes);
        }

        public override void LoadItems(ListBox ItemListBox, List<string> items)
        {
            ItemListBox.LoadItems(items);
        }

        public override void LoadLikeButton(Button likeButton)
        {
            likeButton.Background = ChangeLikeButtonBackground(Liked);
        }

        public override bool SaveChangesLocalCanExecute(object _)
        {
            return Title != "" && ReleaseDate != null && Categories.Count != 0;
        }

        public override void SaveChangesLocalExecute(object parameter)
        {
            appDbContext.SaveChanges();
        }

        public override void SetImageSource(Image image)
        {
            image.SetImageSourceOnLoadFromByteArray(CurentImageBytes);
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System;

namespace PictureCat
{
    public abstract class Album
    {
        protected FlexWrapPanel CurrentPanel = null!;
        protected MainWindow OwnerWindow = null!;
        protected Visibility UploadButtonVisibility;
        protected Visibility AddTagButtonVisibility;
        protected string FolderName = null!;
        protected string[] Images = null!;
        protected Image MainPageImage = null!;
        protected FrameworkElement[] controlosToBlock = null!;
        protected ScrollViewer MainPageScrollViewer = null!;
        protected int LoadCounter;

        public Album(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Visibility uploadButtonVisibility, Visibility addTagButtonVisibility, string folderName, Image mainPageImage)
        {
            CurrentPanel = flexWrapPanel;
            OwnerWindow = ownerWindow;
            UploadButtonVisibility = uploadButtonVisibility;
            AddTagButtonVisibility = addTagButtonVisibility;
            FolderName = folderName;
            MainPageImage = mainPageImage;
            LoadCounter = default(int);
            SetImages();
        }

        public abstract void SetImages();
        public void SetScrollViewer(ScrollViewer scrollViewer)
        {
            MainPageScrollViewer = scrollViewer;
        }
        public abstract Task LoadImageCardsAsync();

        public void SetImagesDynamically(string[] imagesToSet) 
        {
            Images = imagesToSet;
        }

        protected void BlockControls(bool enableStatus)
        {
            foreach (FrameworkElement item in controlosToBlock)
            {
                item.IsEnabled = enableStatus;
            }
        }

        public void SetControlosToBlock(params FrameworkElement[] ControlosToBlock)
        {
            controlosToBlock = ControlosToBlock;
        }

        public virtual int ItemsToLoad() => 30;
    }
}

using PictureCat.CustomViews;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using System.Linq;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace PictureCat
{
    public class UserImagesToAddAlbum : Album
    {
        public UserImagesToAddAlbum(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Image mainPageImage) : base (flexWrapPanel, ownerWindow, Visibility.Visible, Visibility.Collapsed, "UserImagesToAdd", mainPageImage)
        {
            CurrentPanel = flexWrapPanel;
            OwnerWindow = ownerWindow;
        }

        public override void SetImages()
        {
            Images = DirectoryParser.ParseFolder(FolderName)!.ToArray();
        }

        public override async Task LoadImageCardsAsync()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LoadingProgress.InitializeLoading(MainPageImage);
                BlockControls(false);
                MainPageScrollViewer?.ScrollToHome();
            });

            await Task.Run(() =>
            {
                PictureCard pictureCard = null!;
                ImageCardInformation pictureItem = null!;
                if (LoadCounter == 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CurrentPanel.Children.Clear();
                    });
                }
                BitmapImage image = null!;
                int itemsToLoad = ItemsToLoad(),
                    itemsToSkip = LoadCounter * itemsToLoad;
                LoadCounter++;
                string[] newImages = Images.Skip(itemsToSkip).Take(itemsToLoad).ToArray();
                foreach (string item in newImages)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        pictureCard = new PictureCard();        
                    });

                    pictureItem = new ImageToAddCardInformation() { ReleaseDate = DateTime.Now, Path = item, Title = string.Empty };
                    pictureCard.Information = pictureItem;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(item, UriKind.Relative);
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.DecodePixelWidth = 180;
                        image.EndInit();

                        try
                        {
                             pictureCard.CurrentPicture.Source = new CroppedBitmap(image, new Int32Rect(0, 0, 180, 180));
                        }
                        catch
                        {
                            pictureCard.CurrentPicture.Source = image;
                        }

                        pictureCard.Margin = ImageCardInformation.Margin;
                        pictureCard.MouseLeftButtonUp += MouseUp;
                        pictureCard.Style = (Style)Application.Current.FindResource("MyStyle");
                        CurrentPanel.Children.Add(pictureCard);
                    });
                }
                if (Images.Length > (itemsToSkip + itemsToLoad))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        LoadMoreItemsCard moreItemsCard = new LoadMoreItemsCard();
                        moreItemsCard.Margin = ImageCardInformation.Margin;
                        moreItemsCard.MouseLeftButtonUp += MoreItemsCardClick;
                        CurrentPanel.Children.Add(moreItemsCard);
                    });
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                LoadingProgress.FinishLoading(MainPageImage);
                BlockControls(true);
            });
        }

        private async void MoreItemsCardClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() => CurrentPanel.Children.Remove((LoadMoreItemsCard)sender));
                await LoadImageCardsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading images by parts from folder failed");
                if (ex.Message != null)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Inner exeption: {ex.InnerException.Message}");
                }
            }

        }

        private void MouseUp(object sender, RoutedEventArgs e)
        {
            ImageCardInformation? item = ((PictureCard)sender).Information;
            if (item == null) return;
            ImageCardInformation vm = new ImageToAddCardInformation()
            {
                Title = item.Title,
                ReleaseDate = item.ReleaseDate,
                Description = item.Description,
                Liked = item.Liked,
                Path = item.Path,
                Categories = new List<string>(item.Categories),
                Tags = new List<string>(item.Tags)
            };
            SelecedPictureWindow userWindow = new SelecedPictureWindow(vm, UploadButtonVisibility, AddTagButtonVisibility);
            userWindow.Owner = OwnerWindow;
            if (userWindow.ShowDialog() == true)
            {
                item.Title = userWindow.itemInformation.Title;
                item.ReleaseDate = userWindow.itemInformation.ReleaseDate;
                item.Description = userWindow.itemInformation.Description;
                item.Liked = userWindow.itemInformation.Liked;
                item.Path = userWindow.itemInformation.Path;
                item.Categories = userWindow.itemInformation.Categories;
                item.Tags = userWindow.itemInformation.Tags;
            }
            else if (userWindow.DialogResult.HasValue && userWindow.DialogResult.Value == false)
            {
                if (userWindow.itemInformation.Path == null)
                {
                    DirectoryParser.RemoveFromFolder(item.Path);
                    OwnerWindow.MyFlexWrapPanel.Children.Remove(((PictureCard)sender));
                    item.Dispose();
                    ((PictureCard)sender).Information = null!;
                }
            }
            GC.Collect();
        }
    }
}

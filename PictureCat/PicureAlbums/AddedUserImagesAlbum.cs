using PictureCat.CustomViews;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows.Controls;

namespace PictureCat
{
    public class AddedUserImagesAlbum : Album
    {
        private ApplicationDbContext appDbContext = null!;
        private Task loadImageCardsTask = null!;

        public AddedUserImagesAlbum(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Image mainPageImage) : base(flexWrapPanel, ownerWindow, Visibility.Collapsed, Visibility.Visible, "UserImages", mainPageImage)
        {
            CurrentPanel = flexWrapPanel;
            OwnerWindow = ownerWindow;
            appDbContext = ApplicationDbContext.GetInstance();
        }

        public override void SetImages()
        {
            Images = null!;
        }

        public override async Task LoadImageCardsAsync()
        {
            loadImageCardsTask = Task.Run(() =>
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

                    byte[] imageByteArr = appDbContext.Images.Where(i => i.Path == item).Select(i => i.ImageBytes).First();
                    pictureItem = new ImageToCommitCardInformation() { Path = item, CurentImageBytes = imageByteArr };
                    pictureCard.Information = pictureItem;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        using (MemoryStream stream = new MemoryStream(imageByteArr))
                        {
                            image = new BitmapImage();
                            image.BeginInit();
                            image.StreamSource = stream;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.DecodePixelWidth = 140;
                            image.EndInit();
                        }
                        try
                        {
                            pictureCard.CurrentPicture.Source = new CroppedBitmap(image, new Int32Rect(0, 0, 140, 140));
                        }
                        catch
                        {
                            pictureCard.CurrentPicture.Source = image;
                        }
                        pictureCard.Margin = ImageCardInformation.Margin;
                        pictureCard.Style = (Style)Application.Current.FindResource("MyStyle");
                        pictureCard.MouseLeftButtonUp += MouseUp;
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
                LoadingProgress.InitializeLoading(MainPageImage);
                BlockControls(false);
            });
            await loadImageCardsTask;
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
                MessageBox.Show("the process of loading images by parts from database failed");
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

        private async void MouseUp(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!loadImageCardsTask.IsCompleted)
                {
                    return;
                }
                ImageCardInformation? item = ((PictureCard)sender).Information;
                if (item == null)
                {
                    return;
                }
                ImageEntity currentImage = appDbContext.Images.FirstOrDefault(i => i.Path == item.Path)!;
                if (currentImage == null)
                {
                    throw new Exception("This image does not contain any metadata");
                }
                await appDbContext.ImagesToCategories.LoadAsync();
                List<string> ImageCategories = await appDbContext.ImagesToCategories
                    .Where(itc => itc.ImageEntity.Path == item.Path && itc.ImageEntity == currentImage)
                    .Select(itc => itc.CategoryEntity.CategoryName).ToListAsync();
                await appDbContext.ImagesToTags.LoadAsync();
                List<string> ImageTags = await appDbContext.ImagesToTags
                   .Where(itt => itt.ImageEntity.Path == item.Path && itt.ImageEntity == currentImage)
                   .Select(itt => itt.TagEntity.TagName).ToListAsync();

                ImageCardInformation vm = new ImageToCommitCardInformation()
                {
                    Title = currentImage.Title,
                    ReleaseDate = currentImage.ReleaseDate,
                    Description = currentImage.Description,
                    Liked = currentImage.Liked,
                    Path = item.Path,
                    CurentImageBytes = currentImage.ImageBytes,
                    Categories = ImageCategories,
                    Tags = ImageTags
                };

                SelecedPictureWindow userWindow = new SelecedPictureWindow(vm, UploadButtonVisibility, AddTagButtonVisibility);
                userWindow.Owner = OwnerWindow;

                if (userWindow.ShowDialog() == true)
                {
                    List<ImageToCategory> imageToCategories = ImageCardInformation.GetCategoires(ImageCategories);
                    List<ImageToTag> imageToTags = ImageCardInformation.GetTags(ImageTags);

                    currentImage.Title = userWindow.itemInformation.Title;
                    currentImage.ReleaseDate = userWindow.itemInformation.ReleaseDate;
                    currentImage.Description = userWindow.itemInformation.Description;
                    currentImage.Liked = userWindow.itemInformation.Liked;
                    currentImage.ImageCategories = imageToCategories;
                    currentImage.ImageTags = imageToTags;

                    await appDbContext.SaveChangesAsync();
                }
                else if (userWindow.DialogResult.HasValue
                        && userWindow.DialogResult.Value == false
                        && userWindow.itemInformation.Path == null)
                {

                    MessageBoxResult result = MessageBox.Show("Do you want to move this image to 'Basket'? \nIf you choose 'No' the image will be fully removed from programm.",
                        "Attention", MessageBoxButton.YesNoCancel);

                    if (result != MessageBoxResult.Cancel)
                    {
                        OwnerWindow.MyFlexWrapPanel.Children.Remove(((PictureCard)sender));
                        if (result == MessageBoxResult.Yes)
                        {
                            string crurentImagePath = Path.Combine("UserImagesToAdd", currentImage.Path);
                            await DirectoryParser.AddToFolderImageByteArrayAsync(crurentImagePath, currentImage.ImageBytes);
                        }
                        appDbContext.Images.Remove(currentImage);
                        appDbContext.SaveChanges();
                        item.Dispose();
                        ((PictureCard)sender).Information = null!;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading image metadata failed");
                if (ex.Message != null)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Inner exeption: {ex.InnerException.Message}");
                }
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}

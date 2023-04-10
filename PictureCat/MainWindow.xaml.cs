using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfAnimatedGif;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using PictureCat.HelpClassesForGeneralUse;
using System.Windows.Input;

namespace PictureCat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationDbContext appDbContext;

        public MainWindow()
        {
            InitializeComponent();
            appDbContext = ApplicationDbContext.GetInstance();
        }

        private async void AddedPicturesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    UserImagesToAddAlbum album = new UserImagesToAddAlbum(MyFlexWrapPanel, this, MainPageImage);
                    album.SetControlosToBlock(NavigationPanel, SearchButton);
                    album.SetScrollViewer(MyScrollViewer);
                    await album.LoadImageCardsAsync();
                });
                AddedPicturesButton.SetImage(Helper.ImageAdd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading images from database failed");
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

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MyFlexWrapPanel.Children.Count != 0)
            {
                MessageBoxResult result = MessageBox.Show("All information about pictures, that aren't in the database will be lost.", "Are you sure?", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    Clipboard.Clear();
                    // kill the process of unfinished tasks
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                Clipboard.Clear();
            }
        }

        private void MainPageImage_DragEnter(object sender, DragEventArgs e)
        {
            MainPageImage.SetImageSourceFromResources(Helper.ImageDragDrop);
        }

        private void MainPageImage_DragLeave(object sender, DragEventArgs e)
        {
            MainPageImage.SetImageSourceFromResources(Helper.ImageLogo);
        }

        private async void MainPageImage_Drop(object sender, DragEventArgs e)
        {
            try
            {
                LoadingProgress.InitializeLoading(MainPageImage);
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    await DirectoryParser.AddToFolderAsync("UserImagesToAdd", files);
                }
                LoadingProgress.FinishLoading(MainPageImage);
                AddedPicturesButton.SetImage(Helper.ImageRefresh);
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading images to local repository failed");
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

        private async void MainPageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    CategoryAlbum album = new CategoryAlbum(MyFlexWrapPanel, this, MainPageImage);
                    album.SetControlosToBlock(NavigationPanel, SearchButton);
                    album.SetScrollViewer(MyScrollViewer);
                    await album.LoadImageCardsAsync();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading images from database failed");
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

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            MainPageButton_Click(sender, e);
        }

        private async void LikedPicturesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    LikedImagesAlbum album = new LikedImagesAlbum(MyFlexWrapPanel, this, MainPageImage);
                    album.SetControlosToBlock(NavigationPanel, SearchButton);
                    album.SetScrollViewer(MyScrollViewer);
                    await album.LoadImageCardsAsync();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading LIKED images from database failed");
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
        
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchedImagesAlbum album = new SearchedImagesAlbum(MyFlexWrapPanel, this, MainPageImage);
                album.SetControlosToBlock(NavigationPanel, SearchButton);
                album.SetScrollViewer(MyScrollViewer);
                await album.AccessSearch(SearchTextBox.Text);
                album.SetImages();
                await album.LoadImageCardsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading SEARCHED images from database failed");
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

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SearchButton.IsEnabled)
            {
                SearchButton_Click(null!, null!);
            }
        }

        private void CollagePicturesButton_Click(object sender, RoutedEventArgs e)
        {
            CollageWindow collageWindow = new CollageWindow();
            collageWindow.Owner = this;
            collageWindow.ShowDialog();
        }
    }
}

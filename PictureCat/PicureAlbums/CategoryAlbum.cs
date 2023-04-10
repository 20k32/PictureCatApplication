using PictureCat.CustomViews;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace PictureCat
{
    public class CategoryAlbum : Album
    {
        private ApplicationDbContext appDbContext = null!;
        public CategoryAlbum(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Image MainPageImage) 
            : base(flexWrapPanel, ownerWindow, Visibility.Collapsed, Visibility.Visible, string.Empty, MainPageImage)
        {
            appDbContext = ApplicationDbContext.GetInstance();
        }

        public override void SetImages()
        {
            Images = ApplicationDbContext.GetInstance().Categories.Select(x => x.CategoryName).ToArray();
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
                CategoryCard categoryCard = null!;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentPanel.Children.Clear();
                });
                foreach (string item in Images)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        categoryCard = new CategoryCard(item);
                        categoryCard.Margin = ImageCardInformation.Margin;
                        categoryCard.MouseLeftButtonUp += CategoryCardClick;
                        CurrentPanel.Children.Add(categoryCard);
                    });
                   
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AddNewCategoryCard newCategoryCard = new AddNewCategoryCard(OwnerWindow);
                    newCategoryCard.Margin = ImageCardInformation.Margin;
                    newCategoryCard.ImageBorder.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
                    CurrentPanel.Children.Add(newCategoryCard);
                });
                GC.Collect();
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                LoadingProgress.FinishLoading(MainPageImage);
                BlockControls(true);
            });
        }

        private void CategoryCardClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                MainPageScrollViewer?.ScrollToHome();
                AddedUserImagesAlbum currentCategoryAlbum = new AddedUserImagesAlbum(CurrentPanel, OwnerWindow, MainPageImage);
                currentCategoryAlbum.SetControlosToBlock(controlosToBlock);
                string[] currentCategoryImages = 
                    await appDbContext.ImagesToCategories
                    .Where(itc => itc.CategoryEntity.CategoryName == ((CategoryCard)sender).NameTextBlock.Text)
                    .Select(itc => itc.ImageEntity.Path)
                    .ToArrayAsync();

                currentCategoryAlbum.SetImagesDynamically(currentCategoryImages);
                await currentCategoryAlbum.LoadImageCardsAsync();
            });
            GC.Collect();
        }

        private async void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
                AddTag addTagForm = new AddTag(Visibility.Collapsed);
                addTagForm.Owner = OwnerWindow;
                addTagForm.ShowDialog();

                if (addTagForm.DialogResult == true)
                {
                    string newCategoryTagName = addTagForm.NewTagTextBox.Text;

                    if (addTagForm.CatTagComboBox.SelectedIndex == 1)
                    {
                        await Task.Run(() => Helper.ResetIdentityForTags());
                        if (appDbContext.Tags.FirstOrDefault(t => t.TagName == newCategoryTagName) == null)
                        {
                            await appDbContext.Tags.AddAsync(new TagEntity() { TagName = newCategoryTagName });
                            await appDbContext.SaveChangesAsync();
                        }
                        else
                        {
                            MessageBox.Show("There is such tag!");
                        }
                    }
                    else
                    {
                        await Task.Run(() => Helper.ResetIdentityForCategories());
                        if (appDbContext.Categories.FirstOrDefault(c => c.CategoryName == newCategoryTagName) == null)
                        {
                            await appDbContext.Categories.AddAsync(new CategoryEntity() { CategoryName = newCategoryTagName });
                            await appDbContext.SaveChangesAsync();
                            await appDbContext.Categories.LoadAsync();
                            SetImages();
                            await LoadImageCardsAsync();
                        }
                        else
                        {
                            MessageBox.Show("There is such category!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of adding new category/tag to database failed");
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
    }
}

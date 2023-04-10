using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PictureCat.HelpClassesForGeneralUse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace PictureCat
{
    /// <summary>
    /// Логика взаимодействия для SelecedPictureWindow.xaml
    /// </summary>
    public partial class SelecedPictureWindow : Window
    {
        private FullSizeImage imageForm = null!;

        private ApplicationDbContext appDbContext = null!;

        public ImageCardInformation itemInformation = null!;

        public SelecedPictureWindow(ImageCardInformation ItemInformation, Visibility UploadButtonVisibility, Visibility AddTagButtonVisibility)
        {
            InitializeComponent();
            appDbContext = ApplicationDbContext.GetInstance();
            itemInformation = ItemInformation;
            DataContext = itemInformation;
            itemInformation.SetImageSource(SelectedImage);
            UploadButton.Visibility = UploadButtonVisibility;
            AddDeleteCategoryTagPanel.Visibility = AddTagButtonVisibility;
            AddToCollageButton.Visibility = AddTagButtonVisibility;
        }

        private void CheckColor()
        {
            if (itemInformation.Categories.Count == 0)
            {
                CheckListBox.Background = Brushes.AntiqueWhite;
            }
            else
            {
                CheckListBox.Background = Brushes.White;
            }
        }

        private async void selectedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await itemInformation.LoadTagsAsync(HashTagBox, Tag_CheckBox_Click);
                itemInformation.LoadItems(HashTagBox, itemInformation.Tags);
                await itemInformation.LoadCategoriesAsync(CheckListBox, CheckBox_Click);
                itemInformation.LoadItems(CheckListBox, itemInformation.Categories);
                itemInformation.LoadLikeButton(LikeButton);
                CheckColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of loading images failed");
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

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            itemInformation.Liked = !itemInformation.Liked;
            itemInformation.LoadLikeButton(LikeButton);
        }

        private void SelectedImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imageForm = new FullSizeImage(SelectedImage.Source);
            imageForm.Owner = this;
            imageForm.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            itemInformation.Path = null!;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(TitleTextBox), AdditionalInformationRichTextBox);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(TitleTextBox), this);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            if (checkedBox.IsChecked == true)
            {
                itemInformation.Categories.Add((string)checkedBox.Content);
            }
            else
            {
                itemInformation.Categories.Remove((string)checkedBox.Content);
            }
            CheckColor();
        }

        private bool Validate(string content, ListBox CheckBox)
        {
            foreach (var item in CheckBox.Items)
            {
                if (((CheckBox)item).Content.ToString()!.ToLower() == content.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    AddTag addTagForm = null!;
                    bool? dialogResult = false;
                    bool validate = false;
                    int selectedIndex = 0;
                    Dispatcher.Invoke(() =>
                    {
                        addTagForm = new AddTag(Visibility.Collapsed);
                        addTagForm.Owner = this;
                        addTagForm.Title = "Add";
                        addTagForm.ShowDialog();
                        dialogResult = addTagForm.DialogResult;
                        selectedIndex = addTagForm.CatTagComboBox.SelectedIndex;
                    });
                    if (dialogResult == true)
                    {
                        if (selectedIndex == 1)
                        {
                            string newCheckBoxContent = string.Empty;
                            Dispatcher.Invoke(() =>
                            {
                                newCheckBoxContent = addTagForm.NewTagTextBox.Text;
                                validate = Validate(newCheckBoxContent, HashTagBox);
                            });
                            if (validate)
                            {
                                Helper.ResetIdentityForTags();
                                Dispatcher.Invoke(() =>
                                {
                                    CheckBox checkBoxToAdd = new CheckBox() { Content = newCheckBoxContent, Width = 320d };
                                    checkBoxToAdd.Click += Tag_CheckBox_Click;
                                    HashTagBox.Items.Add(checkBoxToAdd);
                                    appDbContext.Tags.Add(new TagEntity() { TagName = checkBoxToAdd.Content.ToString()! });
                                });
                                await appDbContext.SaveChangesAsync();
                            }
                            else
                            {
                                MessageBox.Show("There is such tag!");
                            }
                        }
                        else
                        {
                            string newCheckBoxContent = string.Empty;
                            Dispatcher.Invoke(() =>
                            {
                                newCheckBoxContent = addTagForm.NewTagTextBox.Text;
                                validate = Validate(newCheckBoxContent, CheckListBox);
                            });
                            if (validate)
                            {
                                Helper.ResetIdentityForCategories();
                                Dispatcher.Invoke(() =>
                                {
                                    CheckBox checkBoxToAdd = new CheckBox() { Content = newCheckBoxContent, Width = 320d };
                                    checkBoxToAdd.Click += CheckBox_Click;
                                    CheckListBox.Items.Add(checkBoxToAdd);
                                    appDbContext.Categories.Add(new CategoryEntity() { CategoryName = checkBoxToAdd.Content.ToString()! });
                                });
                                await appDbContext.SaveChangesAsync();
                            }
                            else
                            {
                                MessageBox.Show("There is such category!");
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of adding tag \\ category failed");
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

        private void Tag_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            if (checkedBox.IsChecked == true)
            {
                itemInformation.Tags.Add((string)checkedBox.Content);
            }
            else
            {
                itemInformation.Tags.Remove((string)checkedBox.Content);
            }
            HashTagBox.SelectedItem = checkedBox;
        }

        private void HandleScrollByWheel(ScrollViewer scrollViewer, MouseWheelEventArgs e)
        {
            if (scrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    scrollViewer.LineUp();
                }
                else
                {
                    scrollViewer.LineDown();
                }
            }
            e.Handled = true;
        }

        private void AdditionalInformationRichTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            HandleScrollByWheel(AdditionalInformationRichTextBox.FindChild<ScrollViewer>(), e);
        }

        private void TitleTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            HandleScrollByWheel(TitleTextBox.FindChild<ScrollViewer>(), e);
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async ValueTask RemoveCategoriesFromDb(List<string> categories)
        {
            await appDbContext.Categories.LoadAsync();
            foreach (var item in categories)
            {
                CategoryEntity catEntity = (CategoryEntity)appDbContext.Categories.Single(x => x.CategoryName == item);
                appDbContext.Categories.Remove(catEntity);
            }
            await appDbContext.SaveChangesAsync();
            Dispatcher.Invoke(() =>
            {
                CheckListBox.Items.Clear();
            });
            await CheckListBox.LoadCategoriesAsync(appDbContext, CheckBox_Click);
            Dispatcher.Invoke(() =>
            {
                itemInformation.LoadItems(CheckListBox, itemInformation.Categories);
            });
        }

        private ValueTask RemoveImageEntitiesFromDb(List<string> images)
        {
            appDbContext.Images.Load();
            foreach (var item in images)
            {
                ImageEntity currentImage = appDbContext.Images.First(i => i.Path == item);
                appDbContext.Images.Remove(currentImage);
            }
            appDbContext.SaveChanges();
            return ValueTask.CompletedTask;
        }

        private async void DeleteTagButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    AddTag addTagForm = null!;
                    bool? dialogResult = false;
                    int selectedIndex = 0;
                    List<string> itemsToDelete = null!;
                    Dispatcher.Invoke(() =>
                    {
                        addTagForm = new AddTag(Visibility.Visible);
                        addTagForm.Owner = this;
                        addTagForm.Title = "Delete";
                        addTagForm.ShowDialog();
                        dialogResult = addTagForm.DialogResult;
                        selectedIndex = addTagForm.CatTagComboBox.SelectedIndex;
                        itemsToDelete = addTagForm.ItemsToDelete;
                    });
                    if (dialogResult == true)
                    {
                        if (selectedIndex == 1)
                        {
                            foreach (var item in itemsToDelete)
                            {
                                TagEntity tagEntity = (TagEntity)appDbContext.Tags.Single(x => x.TagName == item);
                                appDbContext.Tags.Remove(tagEntity);

                                if (itemInformation.Tags.Contains(item))
                                {
                                    itemInformation.Tags.Remove(item);
                                }

                            }
                            await appDbContext.SaveChangesAsync();
                            Dispatcher.Invoke(() =>
                            {
                                HashTagBox.Items.Clear();
                            });
                            await itemInformation.LoadTagsAsync(HashTagBox, Tag_CheckBox_Click);
                            Dispatcher.Invoke(() =>
                            {
                                itemInformation.LoadItems(HashTagBox, itemInformation.Tags);
                            });
                        }
                        else
                        {
                            List<string> imagesWithOneCategory = new List<string>();
                            MessageBoxResult messageBoxResult;
                            List<string> ImagesToDelete = new List<string>();

                            var imagesWithRemovingCategory = await appDbContext.ImagesToCategories
                                .Where(itc => itemsToDelete.Contains(itc.CategoryEntity.CategoryName))
                                .ToArrayAsync()!;

                            await appDbContext.Images.LoadAsync();

                            string[] imagesWithOneRemovingCategory = imagesWithRemovingCategory
                                .Where(itc => itc.ImageEntity.ImageCategories.Count == 1)
                                .Select(itc => itc.ImageEntity.Path).ToArray();

                            await appDbContext.Categories.LoadAsync();

                            string[] imagesWithMoreRemovingCategories = imagesWithRemovingCategory
                                .Where(itc => itc.ImageEntity.ImageCategories.Select(x => x.CategoryEntity.CategoryName).OrderBy(x => x)
                                .SequenceEqual(itemsToDelete.OrderBy(x => x)))
                                .Select(itc => itc.ImageEntity.Path).Distinct().ToArray();


                            if (imagesWithOneRemovingCategory != null)
                            {
                                imagesWithOneCategory.AddRange(imagesWithOneRemovingCategory);
                            }
                            if (imagesWithMoreRemovingCategories != null)
                            {
                                imagesWithOneCategory.AddRange(imagesWithMoreRemovingCategories);
                            }
                            if (imagesWithOneCategory.Count > 0)
                            {
                                messageBoxResult = MessageBox.Show("There are images that have only one category marked for removal." +
                                    "\nDo you want to move this images to 'Basket'?" +
                                    "\nIf you choose 'No', such images will be completely deleted from programm.", "Attention", MessageBoxButton.YesNoCancel);
                                if (messageBoxResult != MessageBoxResult.Cancel)
                                {

                                    List<byte[]> byteArrays = imagesWithRemovingCategory
                                    .Where(i => imagesWithOneRemovingCategory!.Contains(i.ImageEntity.Path)
                                            || imagesWithMoreRemovingCategories!.Contains(i.ImageEntity.Path))
                                    .Select(i => i.ImageEntity.ImageBytes).Distinct().ToList();

                                    if (messageBoxResult == MessageBoxResult.Yes)
                                    {
                                        DirectoryParser.AddToFolderImageByteArrayRangeSync("UserImagesToAdd", byteArrays, imagesWithOneCategory);
                                    }
                                    await RemoveCategoriesFromDb(itemsToDelete);
                                    await RemoveImageEntitiesFromDb(imagesWithOneCategory);
                                }
                            }
                            else
                            {
                                await RemoveCategoriesFromDb(itemsToDelete);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of deleting tag \\ category failed");
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

        private void Completed(object sender, EventArgs e)
        {
            PictureBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 58, 58, 58));
        }

        private void SelectedImage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            itemInformation.CopyImageToClipBoard();
            BorderAnimation.DoReverseAnimation(Completed!, PictureBorder);
        }

        private void selectedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GC.Collect();
        }

        private async void AddToCollageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await DirectoryParser.AddToFolderImageByteArrayAsync(Path.Combine("UserImagesCollage", itemInformation.Path), itemInformation.CurentImageBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of adding image to collage failed");
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

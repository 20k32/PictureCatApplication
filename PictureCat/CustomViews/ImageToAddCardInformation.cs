using DocumentFormat.OpenXml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PictureCat.HelpClassesForGeneralUse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PictureCat.CustomViews
{
    internal class ImageToAddCardInformation : ImageCardInformation
    {
        public Task addToDbLocalTask = null!;

        public ImageToAddCardInformation()
        {
            AddToDbCommand = new MyRoutedCommand(AddToDbCommandExecute, AddToDbCommandCanExecute);
            SaveChangesLocal = new MyRoutedCommand(SaveChangesLocalExecute, SaveChangesLocalCanExecute);
        }

        // так как добавление нового изображения в базу - процесс долгий из-за поиска места для вставки (избегание переполнения автоикрементного поля)
        // я решил сделать его синхронным, этот метод блокирует поток пользовательского интерфейса до тех пор пока метод не выполниться
        // такое решение проблемы имеет ряд недостатков, такие как нефункциональность программы, неотзывчивость пользовательского интерфейса, но
        // 1. проблема заметна на 2000+ картинках в базе данных ( зависит от мощности процессора )
        // 2. добавление двух картинок в базу данных из разных потоков (ОДНОВРЕМЕННО), обращение к базе данных из разных потоков (ОДНОВРЕМЕННО) не предусмотрено логикой работы программы и реализация такого механизма накладна из-за пересчета автоикрементного поля
        // и 'потоко-не-безопасности' класса контекста базы данных.
        // 3. из-за п.2 очень важно блокировать поток пользовательского интефейса чтоб пользователь не мог послать контексту бд, который занят работой, другой запрос на  выполнение (приводит к ошибке и неоднозначному завершению работы операции в другом потоке).

        public override void AddToDbCommandExecute(object obj)
        {
            string tempPath = this.Path;
            try
            {
                tempPath = this.Path;
                this.Path = null!;

                byte[] imageBytes = File.ReadAllBytes(tempPath);
                List<ImageToCategory> imageToCategories = GetCategoires(Categories);
                List<ImageToTag> imageToTags = GetTags(Tags);

                Helper.ResetIdentityForImages();

                ImageEntity imageEntity = new ImageEntity()
                {
                    Title = this.Title,
                    Path = tempPath.AsSpan(39).ToString(),
                    Description = this.Description == null ? string.Empty : this.Description,
                    ReleaseDate = this.ReleaseDate,
                    Liked = this.Liked,
                    ImageBytes = imageBytes,
                    ImageCategories = imageToCategories,
                    ImageTags = imageToTags
                };

                if (appDbContext.Images.Where(i => i.Path == imageEntity.Path).FirstOrDefault() != null)
                {
                    throw new Exception($"The image with name '{imageEntity.Path}' already exists in database!");
                }

                appDbContext.ImagesToCategories.AddRange(imageToCategories);
                appDbContext.ImagesToTags.AddRange(imageToTags);
                appDbContext.Images.Add(imageEntity);
                appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("the process of adding a new image failed");
                if (ex.Message != null)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Inner exeption: {ex.InnerException.Message}");
                }
                this.Path = tempPath;
            }
        }

        public override ImageCardInformation Clone()
        {
            return new ImageToAddCardInformation();
        }

        public override void CopyImageToClipBoard()
        {
            Helper.CopyImageToClipBoard(Path.AsSpan(39).ToString());
        }

        public override void LoadItems(ListBox ItemListBox, List<string> items)
        {
            ItemListBox.LoadItems(items);
        }

        public override void LoadLikeButton(Button likeButton)
        {
            likeButton.Background = ChangeLikeButtonBackground(Liked);
        }

        public override bool SaveChangesLocalCanExecute(object parameter)
        {
            return true;
        }

        public override void SaveChangesLocalExecute(object parameter)
        {
            
        }

        public override void SetImageSource(Image image)
        {
            image.SetImageSourceOnLoad(Path);
        }

        private bool AddToDbCommandCanExecute(object _)
        {
            return Title != "" && ReleaseDate != null && Categories.Count != 0;
        }
    }
}

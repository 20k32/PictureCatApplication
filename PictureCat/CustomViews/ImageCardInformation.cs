using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using PictureCat.HelpClassesForGeneralUse;
using System.Windows.Media;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PictureCat
{
    public abstract class ImageCardInformation : INotifyPropertyChanged
    {
        public static readonly Thickness Margin = new Thickness(10);
        public List<string> Categories = new List<string>();
        public List<string> Tags = new List<string>();
        public ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();

        public byte[] CurentImageBytes = null!;

        private string title = null!;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private bool liked;
        public bool Liked
        {
            get => liked;
            set
            {
                liked = value;
                OnPropertyChanged("Liked");
            }
        }

        private DateTime? releaseDate;
        public DateTime? ReleaseDate
        {
            get => releaseDate;
            set
            {
                releaseDate = value;
                OnPropertyChanged("ReleaseDate");
            }
        }

        private string description = null!;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string path = null!;
        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand AddToDbCommand { get; set; } = null!;

        public abstract void AddToDbCommandExecute(object parameter);

        public ICommand SaveChangesLocal { get; set; } = null!;

        public abstract void SaveChangesLocalExecute(object parameter);
        public abstract bool SaveChangesLocalCanExecute(object parameter);

        public abstract ImageCardInformation Clone();

        public async ValueTask LoadCategoriesAsync(ListBox CatetoryListBox, RoutedEventHandler chekBoxClick)
        {
            await CatetoryListBox.LoadCategoriesAsync(appDbContext, chekBoxClick);
        }

        public async ValueTask LoadTagsAsync(ListBox TagListBox, RoutedEventHandler chekBoxClick)
        {
            await TagListBox.LoadTagsAsync(appDbContext, chekBoxClick);
        }

        public abstract void LoadItems(ListBox ItemListBox, List<string> items);

        public abstract void LoadLikeButton(Button likeButton);

        protected SolidColorBrush ChangeLikeButtonBackground(bool liked)
        {
            return liked ? new SolidColorBrush(Colors.LightPink) : new SolidColorBrush(Colors.LightGray);
        }

        public void Dispose()
        {
            Categories = null!;
            Tags = null!;
            appDbContext = null!;
        }

        public static List<ImageToCategory> GetCategoires(List<string> categories)
        {
            ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
            List<ImageToCategory> resultList = new List<ImageToCategory>();
            appDbContext.Categories.Load();
            for (int i = 0; i < categories.Count; i++)
            {
                CategoryEntity existingCategory = appDbContext.Categories.FirstOrDefault(c => c.CategoryName == categories[i])!;
                if (existingCategory != null)
                {
                    resultList.Add(new ImageToCategory()
                    {
                        CategoryEntity = existingCategory
                    });
                }
            }
            return resultList;
        }

        public static List<ImageToTag> GetTags(List<string> tags)
        {
            ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
            List<ImageToTag> resultList = new List<ImageToTag>();
            appDbContext.Tags.Load();
            for (int i = 0; i < tags.Count; i++)
            {
                TagEntity existingTag = appDbContext.Tags.FirstOrDefault(t => t.TagName == tags[i])!;
                if (existingTag != null)
                {
                    resultList.Add(new ImageToTag()
                    {
                        TagEntity = existingTag
                    });
                }
            }
            return resultList;
        }

        public abstract void SetImageSource(Image image);

        public abstract void CopyImageToClipBoard();
    }
}

using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;

namespace PictureCat
{
    public static class Helper
    {
        public static readonly string ImageLogo = "Images\\Logo.png";
        public static readonly string ImageDragDrop = "Images\\DragDrop.png";
        public static readonly string ImageRefresh = "Images\\refresh.png";
        public static readonly string ImageAdd = "Images\\addedpage.png";
        public static readonly string CurrentDirectory = null!;

        static Helper()
        {
            int unusedDirectoryPathLength = 0;
            #if DEBUG
                unusedDirectoryPathLength = 25;
            #else
                unusedDirectoryPathLength = 27;
            #endif
            CurrentDirectory =
                AppDomain.CurrentDomain.BaseDirectory.AsSpan(0, AppDomain.CurrentDomain.BaseDirectory.Length - unusedDirectoryPathLength)
                .ToString();
        }


        public static void SetImage(this Button button, string RelativeImagePath)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(RelativeImagePath, UriKind.Relative));
            button.Content = image;
        }

        public static T FindChild<T>(this DependencyObject parent) where T : UIElement
        {
            if (parent is T) return (T)parent;
            int children = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < children; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!(child is T))
                {
                    T tChild = FindChild<T>(child);
                    if (tChild != null) return tChild;
                }
                else return (child as T)!;
            }
            return null!;
        }

        public static void CopyImageToClipBoard(string imageFilePath)
        {
            Clipboard.Clear();
            string directoryPath = Path.Combine(CurrentDirectory, "UserImagesToAdd", imageFilePath);
            var fileDropList = new StringCollection();
            fileDropList.Add(directoryPath);
            Clipboard.SetFileDropList(fileDropList);
        }

        public static void CopyImageToClipBoardFromByteArray(string imagePath, byte[] array)
        {
            Clipboard.Clear();
            string directoryPath = Path.Combine(CurrentDirectory, "UserImages");
            string file = Directory.GetFiles(directoryPath).FirstOrDefault()!;
            if (file != null)
            {
                File.Delete(file);
            }
            string filePath = Path.Combine(directoryPath, imagePath);
            File.WriteAllBytes(filePath, array);
            DataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.FileDrop, new string[] { filePath }); 
            Clipboard.SetDataObject(dataObject, true);
        }

        public static void ResetIdentityForTags()
        {
            ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
            int iterator = 0;
            while (true)
            {
                var nexRecord = appDbContext.Tags.Find(++iterator);
                if (nexRecord == null)
                {
                    break;
                }
            }
            appDbContext.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT('[Tags]', RESEED, {iterator - 1})");
        }

        public static void ResetIdentityForImages()
        {
            ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
            int iterator = 0;
            if (appDbContext.ImagesToCategories.FirstOrDefault() == null)
            {
                iterator = 2;
            }
            else
            {
                while (true)
                {
                    var nexRecord = appDbContext.Images.Find(++iterator);
                    if (nexRecord == null)
                    {
                        break;
                    }
                }
            }
            appDbContext.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT('[Images]', RESEED, {iterator - 1})");
        }

        public static void ResetIdentityForCategories()
        {
            ApplicationDbContext appDbContext = ApplicationDbContext.GetInstance();
            int iterator = 0;
            while (true)
            {
                var nexRecord = appDbContext.Categories.Find(++iterator);
                if (nexRecord == null)
                {
                    break;
                }
            }
            appDbContext.Database.ExecuteSqlInterpolated($"DBCC CHECKIDENT('[Category]', RESEED, {iterator - 1})");
        }
    }
}

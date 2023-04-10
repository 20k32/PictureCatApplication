using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Math;
using System.Text.RegularExpressions;
using System.Text;
using System;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PictureCat
{
    public class SearchedImagesAlbum : Album
    {
        private ApplicationDbContext appDbContext = null!;
        private string[] SearchedImages = null!;
        // max count of searched items
        public const int NUMBER_OF_ITEMS_TAKEN = 100;

        public SearchedImagesAlbum(FlexWrapPanel flexWrapPanel, MainWindow ownerWindow, Image MainPageImage) 
            : base(flexWrapPanel, ownerWindow, Visibility.Collapsed, Visibility.Visible, string.Empty, MainPageImage)
        {
            appDbContext = ApplicationDbContext.GetInstance();
        }

        private string PrepareSearchOptionsString(string searchOptions)
        {
            string resultString = null!;
            if (searchOptions == string.Empty)
            {
                return null!;
            }
            StringBuilder stringBuilder = new StringBuilder();
            Regex rgx = new Regex("[^a-zA-Zа-яА-я0-9#ії'єІЇЄ]");
            searchOptions = rgx.Replace(searchOptions, " ");
            resultString = Regex.Replace(searchOptions, " {1,}", " ");
            
            return resultString;
        }

        public async Task AccessSearch(string searchOptions)
        {
            string preparedOptions = PrepareSearchOptionsString(searchOptions);
            List<string> searchedImagesList = new List<string>();
            string[] tags = Regex.Matches(preparedOptions, @"#\w+").Select(x => x.Value).ToArray();
            string[] imagesByNameDescription = null!;
            string[] imagesByCategoryTag = null!;
            string compareItem = null!;

            foreach (string item in tags)
            {
                preparedOptions.Replace(item, "");
            }
            
            string[] otherOptions = preparedOptions.Split(' ');

            if (otherOptions.Length > 0)
            {
                if (otherOptions[0].Length == 4 && int.TryParse(otherOptions[0], out int year))
                {
                    imagesByNameDescription =
                       await appDbContext.Images
                       .Where(i => i.ReleaseDate.Value.Year == year)
                       .Select(i => i.Path).ToArrayAsync();

                    if (imagesByNameDescription.Length != 0)
                    {
                        searchedImagesList.AddRange(imagesByNameDescription);
                        searchedImagesList = searchedImagesList.ToList();
                    }
                }

                if (otherOptions.Length == 3 && 
                    DateTime.TryParse(string.Concat(otherOptions[0], ".", otherOptions[1], ".", otherOptions[2]), 
                    out DateTime currentImageDate))
                {
                    imagesByNameDescription =
                       await appDbContext.Images
                       .Where(i => i.ReleaseDate == currentImageDate)
                       .Select(i => i.Path).ToArrayAsync();

                    if (imagesByNameDescription.Length != 0)
                    {
                        searchedImagesList.AddRange(imagesByNameDescription);
                        searchedImagesList = searchedImagesList.ToList();
                    }
                }
            }
            foreach (string item in otherOptions)
            {
                compareItem = item.ToLower();

                imagesByNameDescription = 
                    await appDbContext.Images
                    .Where(i => i.Title.Contains(compareItem) || i.Description.Contains(compareItem))
                    .Select(i => i.Path).ToArrayAsync();

                imagesByCategoryTag =
                    await appDbContext.ImagesToCategories
                    .Where(itc => itc.CategoryEntity.CategoryName.ToLower() == compareItem)
                    .Select(itc => itc.ImageEntity.Path).ToArrayAsync();

                if (imagesByNameDescription.Length != 0)
                {
                    searchedImagesList.AddRange(imagesByNameDescription);
                    searchedImagesList = searchedImagesList.Distinct().ToList();
                }
                if (imagesByCategoryTag.Length != 0)
                {
                    searchedImagesList.AddRange(imagesByCategoryTag);
                    searchedImagesList = searchedImagesList.Distinct().ToList();
                }

            }

            foreach (string item in tags)
            {
                compareItem = item.ToLower();
                imagesByCategoryTag =
                    await appDbContext.ImagesToTags
                    .Where(itt => itt.TagEntity.TagName.ToLower() == compareItem)
                    .Select(itt => itt.ImageEntity.Path).ToArrayAsync();

                if (imagesByCategoryTag.Length != 0)
                {
                    searchedImagesList.AddRange(imagesByCategoryTag);
                    searchedImagesList = searchedImagesList.Distinct().ToList();
                }
            }
            // из результата взять только NUMBER_OF_ITEMS_TAKEN совпадений для оптимизации.
            SearchedImages = searchedImagesList?.Take(NUMBER_OF_ITEMS_TAKEN).ToArray()!;
        }

        public override Task LoadImageCardsAsync()
        {
            if (Images == null)
            {
                return Task.CompletedTask;
            }
            Task loadImageCardsTask = null!; 
            Application.Current.Dispatcher.Invoke(async () =>
            {
                MainPageScrollViewer?.ScrollToHome();
                AddedUserImagesAlbum currentCategoryAlbum = new AddedUserImagesAlbum(CurrentPanel, OwnerWindow, MainPageImage);
                currentCategoryAlbum.SetControlosToBlock(controlosToBlock);
                currentCategoryAlbum.SetImagesDynamically(Images);
                loadImageCardsTask = currentCategoryAlbum.LoadImageCardsAsync();
                await loadImageCardsTask;
            });
            GC.Collect();
            return loadImageCardsTask;
        }

        public override void SetImages()
        {
            Images = SearchedImages;
        }
    }
}

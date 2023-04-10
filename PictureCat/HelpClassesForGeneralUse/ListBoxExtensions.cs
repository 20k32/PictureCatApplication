using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PictureCat.HelpClassesForGeneralUse
{
    public static class ListBoxExtensions
    {
        /// <summary>
        /// Use this method only with checklistboxes
        /// </summary>
        public static void LoadItems(this ListBox currentListBox, List<string> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                foreach (var item in currentListBox.Items)
                {
                    if ((string)((CheckBox)item).Content == categories[i])
                    {
                        ((CheckBox)item).IsChecked = true;
                        break;
                    }
                }
            }
        }

        public static void LoadTags(this ListBox currentListBox, ApplicationDbContext context, RoutedEventHandler checkBoxClick)
        {
            List<string> tags = context.Tags.Select(x => x.TagName).ToList();
            List<CheckBox> checkBoxesToAdd = new List<CheckBox>();
            foreach (var item in tags)
            {
                if (currentListBox.Items.Count == 0)
                {
                    CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                    checkBoxToAdd.Click += checkBoxClick;
                    currentListBox.Items.Add(checkBoxToAdd);
                }
                else
                {
                    foreach (var listBoxItem in currentListBox.Items)
                    {
                        if ((string)((CheckBox)listBoxItem).Content != item)
                        {
                            CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                            checkBoxToAdd.Click += checkBoxClick;
                            checkBoxesToAdd.Add(checkBoxToAdd);
                        }
                    }
                }
            }
            foreach (var checkBoxItem in checkBoxesToAdd)
            {
                currentListBox.Items.Add(checkBoxItem);
            }
        }

        public static async ValueTask LoadTagsAsync(this ListBox currentListBox, ApplicationDbContext context, RoutedEventHandler checkBoxClick)
        {
            await Task.Run(() =>
            {
                List<string> tags = context.Tags.Select(x => x.TagName).ToList();
                List<CheckBox> checkBoxesToAdd = new List<CheckBox>();
                foreach (var item in tags)
                {
                    if (currentListBox.Items.Count == 0)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                            checkBoxToAdd.Click += checkBoxClick;
                            currentListBox.Items.Add(checkBoxToAdd);
                        });
                    }
                    else
                    {
                        foreach (var listBoxItem in currentListBox.Items)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if ((string)((CheckBox)listBoxItem).Content != item)
                                {

                                    CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                                    checkBoxToAdd.Click += checkBoxClick;
                                    checkBoxesToAdd.Add(checkBoxToAdd);

                                }
                            });
                        }
                    }
                }
                foreach (var checkBoxItem in checkBoxesToAdd)
                {
                    Application.Current.Dispatcher.Invoke(() => currentListBox.Items.Add(checkBoxItem));
                }
            });
        }

        public static async ValueTask LoadCategoriesAsync(this ListBox currentListBox, ApplicationDbContext context, RoutedEventHandler checkBoxClick)
        {
            await Task.Run(() =>
            {
                List<string> categories = context.Categories.Select(x => x.CategoryName).ToList();
                List<CheckBox> checkBoxesToAdd = new List<CheckBox>();
                foreach (var item in categories)
                {
                    if (currentListBox.Items.Count == 0)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                            checkBoxToAdd.Click += checkBoxClick;
                            currentListBox.Items.Add(checkBoxToAdd);
                        });
                    }
                    else
                    {
                        foreach (var listBoxItem in currentListBox.Items)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if ((string)((CheckBox)listBoxItem).Content != item)
                                {

                                    CheckBox checkBoxToAdd = new CheckBox() { Content = item, Width = 320d };
                                    checkBoxToAdd.Click += checkBoxClick;
                                    checkBoxesToAdd.Add(checkBoxToAdd);

                                }
                            });
                        }
                    }
                }
                foreach (var checkBoxItem in checkBoxesToAdd)
                {
                    Application.Current.Dispatcher.Invoke(() => currentListBox.Items.Add(checkBoxItem));
                }
            });
        }
    }
}

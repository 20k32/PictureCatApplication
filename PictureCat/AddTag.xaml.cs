using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PictureCat.HelpClassesForGeneralUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PictureCat
{
    /// <summary>
    /// Логика взаимодействия для AddTag.xaml
    /// </summary>
    public partial class AddTag : Window
    {

        public List<string> ItemsToDelete = null!;

        private Visibility ReverseVisibility(Visibility visibility)
        {
            return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }


        public AddTag(Visibility deletionPanelVisibility)
        {
            InitializeComponent();
            AddStackPanel.Visibility = ReverseVisibility(deletionPanelVisibility);
            DeleteStackPanel.Visibility = deletionPanelVisibility;
            if (DeleteStackPanel.Visibility == Visibility.Visible)
            {
                ItemsToDelete = new List<string>();
                CatTagComboBox.SelectionChanged += CatTagComboBox_SelectionChanged;
            }
        }

        private async void CatTagComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeletionListBox.Items.Clear();
            ItemsToDelete = new List<string>();
            if (CatTagComboBox.SelectedIndex == 0)
            {
                await DeletionListBox.LoadCategoriesAsync(ApplicationDbContext.GetInstance(), CheckBox_Click);
            }
            else
            {
                await DeletionListBox.LoadTagsAsync(ApplicationDbContext.GetInstance(), CheckBox_Click);
            }
        }

        private async void AddForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (DeleteStackPanel.Visibility == Visibility.Visible)
            {
                await DeletionListBox.LoadCategoriesAsync(ApplicationDbContext.GetInstance(), CheckBox_Click);
            }
            else
            {
                NewTagTextBox.Focus();
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkedBox = (CheckBox)sender;
            if (checkedBox.IsChecked == true)
            {
                ItemsToDelete.Add((string)checkedBox.Content);
            }
            else
            {
                ItemsToDelete.Remove((string)checkedBox.Content);
            }
        }


        private string PrepareString(string BeginChar)
        {
            if (NewTagTextBox.Text == string.Empty)
            {
                return null!;
            }
            string result = NewTagTextBox.Text;
            StringBuilder stringBuilder = new StringBuilder();
            result = Regex.Replace(result, " {1,}", " ");
            Regex rgx = new Regex("[^a-zA-Zа-яА-я0-9ії'єІЇЄ]");
            result = rgx.Replace(result, "");
            if (result == "")
            {
                return null!;
            }
            stringBuilder.Append(BeginChar);
            stringBuilder.Append(result[0]);
            if (result.Length > 0)
            {
                stringBuilder.Append(result.AsSpan(1).ToString());
            }
            return stringBuilder.ToString();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string result = null!;
            if (CatTagComboBox.SelectedIndex == 0)
            {
                result = PrepareString(string.Empty);
            }
            else
            {
                result = PrepareString("#");
            }
            if (result != null)
            {
                NewTagTextBox.Text = result;
                DialogResult = true;
            }  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsToDelete.Count > 0)
            {
                DialogResult = true;
            }
        }

        private void NewTagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitButton_Click(null!, null!);
            }
        }
    }
}

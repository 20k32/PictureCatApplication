using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace PictureCat
{
    /// <summary>
    /// Логика взаимодействия для CollageWindow.xaml
    /// </summary>
    /// 


    //todo: в коллаже изначально будут макеты:
    // можно будет выбрать коллаж из двух картинок, из трёх, четырёх, пяти, шести - выбор сделать комбобоксом
    // при выборе будет отрисовываться сетка, в сетку драг дропом помещать картинки

    public class MyListItem
    {
        public ImageSource path { get; set; } = null!;
        public string title { get; set; } = null!;

        public MyListItem(ImageSource Path, string Title)
        {
            path = Path;
            title = Title;
        }

        public override string ToString()
        {
            return title;
        }
    }

    public partial class CollageWindow : Window
    {
        private ListBoxItem mouseOverListBoxItem = null!;
        private string[] images = null!;
        private const string folderName = "UserImagesCollage";
        private bool allowDrop = false;

        private ColumnDefinition columnDefinition21 = null!;
        private ColumnDefinition columnDefinition22 = null!;
        private ColumnDefinition columnDefinition61 = null!;
        
        private RowDefinition rowDefinition41 = null!;
        private RowDefinition rowDefinition42 = null!;
        private RowDefinition rowDefinition61 = null!;

        public CollageWindow()
        {
            InitializeComponent();

            columnDefinition21 = new ColumnDefinition();
            columnDefinition22 = new ColumnDefinition();
            columnDefinition61 = new ColumnDefinition();

            rowDefinition41 = new RowDefinition();
            rowDefinition42 = new RowDefinition();
            rowDefinition61 = new RowDefinition();

            columnDefinition21.Width = new GridLength(1, GridUnitType.Star);
            columnDefinition22.Width = new GridLength(1, GridUnitType.Star);
            columnDefinition61.Width = new GridLength(1, GridUnitType.Star);

            rowDefinition41.Height = new GridLength(1, GridUnitType.Star);
            rowDefinition42.Height = new GridLength(1, GridUnitType.Star);
            rowDefinition61.Height = new GridLength(1, GridUnitType.Star);
        }

        public async Task LoadImagesFromFolder()
        {
            ImageListBox.Items.Clear();
            await Task.Run(() =>
            {
                images = DirectoryParser.ParseFolder(folderName)!.ToArray();
                foreach (string item in images)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(item, UriKind.Relative);
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.DecodePixelWidth = 50;
                        image.EndInit();
                        ImageSource source = null!;

                        try
                        {
                            source = new CroppedBitmap(image, new Int32Rect(0, 0, 50, 50));
                        }
                        catch
                        {
                            source = image;
                        }
                        ImageListBox.Items.Add(new MyListItem(source, item.AsSpan(41).ToString()));
                    });
                }
            });
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImagesFromFolder();
            DelayBar.Value = 2;
            DelayBar_ValueChanged(DelayBar, null!);
            GridCeckBox.IsChecked = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((MyListItem)ImageListBox.SelectedItem != null)
                {
                    string fileName = "\\" + ((MyListItem)ImageListBox.SelectedItem).title;
                    DirectoryParser.RemoveFileFromFolder(folderName, fileName);
                    ImageListBox.Items.Remove(((MyListItem)ImageListBox.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ImageListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            allowDrop = true;
            mouseOverListBoxItem.IsSelected = true;
            DragDrop.DoDragDrop((ListBox)sender, mouseOverListBoxItem.Content.ToString(), DragDropEffects.Move);
        }

        private void ImageListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var element = e.OriginalSource as UIElement;
            while (element != null && element != (ListBox)sender)
            {
                if (element is ListBoxItem listBoxItem)
                {
                    mouseOverListBoxItem = listBoxItem;
                    break;
                }
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }
        }

        private void CollageCanvas_Drop(object sender, DragEventArgs e)
        {
            if (allowDrop)
            {
                CanvasGrid_Drop(sender, e);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Save collage as",
                CheckPathExists = true,
                DefaultExt = "png",
                Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|All files (*.*)|*.*"
            };

            if (sfd.ShowDialog() == true)
            {
                CanvasGrid.ShowGridLines = false;
                CanvasGrid.UpdateLayout();
                RenderVisualService.RenderToPNGFile(CollageCanvas, sfd.FileName);
                CanvasGrid.ShowGridLines = GridCeckBox.IsChecked!.Value;
            }

        }

        private void CanvasGrid_Drop(object sender, DragEventArgs e)
        {

            if (!allowDrop)
            {
                return;
            }

            Point pos = e.GetPosition(CanvasGrid);

            int row = -1;
            int col = -1;

            if (CanvasGrid.RowDefinitions.Count != 0)
            {

                for (int i = 0; i < CanvasGrid.RowDefinitions.Count; i++)
                {
                    if (pos.Y > CanvasGrid.RowDefinitions[i].ActualHeight)
                    {
                        pos.Y -= CanvasGrid.RowDefinitions[i].ActualHeight;
                    }
                    else
                    {
                        row = i;
                        break;
                    }
                }
            }
            else
            {
                row = 0;
            }

            if (CanvasGrid.ColumnDefinitions.Count != 0)
            {
                for (int i = 0; i < CanvasGrid.ColumnDefinitions.Count; i++)
                {
                    if (pos.X > CanvasGrid.ColumnDefinitions[i].ActualWidth)
                    {
                        pos.X -= CanvasGrid.ColumnDefinitions[i].ActualWidth;
                    }
                    else
                    {
                        col = i;
                        break;
                    }
                }
            }
            else
            {
                col = 0;
            }

            if (row != -1 && col != -1)
            {
                string? text = e.Data.GetData(DataFormats.Text) as string;
                if (text == null)
                {
                    return;
                }

                UIElement element = CanvasGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col)!;
                if (element != null && element.GetType().Equals(typeof(Image)))
                {
                    CanvasGrid.Children.Remove(element);
                }

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(Path.Combine(DirectoryParser.PATH_TO_FOLDER, "UserImagesCollage", text), UriKind.Relative);
                image.CacheOption = BitmapCacheOption.OnLoad;

                int decodedPixelWidth = default(int);
                if (CanvasGrid.ColumnDefinitions.Count > 0)
                {
                    decodedPixelWidth = (int)CanvasGrid.ColumnDefinitions[0].ActualWidth;
                }
                if (CanvasGrid.RowDefinitions.Count > 0 && decodedPixelWidth > CanvasGrid.RowDefinitions[0].ActualHeight)
                {
                    decodedPixelWidth = (int)CanvasGrid.RowDefinitions[0].ActualHeight;
                }
                image.DecodePixelWidth = decodedPixelWidth;
                image.EndInit();

                Image bodyImage = new Image()
                {
                    Source = image,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };
                Grid.SetRow(bodyImage, row);
                Grid.SetColumn(bodyImage, col);
                CanvasGrid.Children.Add(bodyImage);
                allowDrop = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CanvasGrid.ShowGridLines = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CanvasGrid.ShowGridLines = false;
        }

        private void DelayBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CanvasGrid != null)
            {
                if (CanvasGrid.Children != null)
                {
                    CanvasGrid.Children.Clear();
                }
                CanvasGrid.RowDefinitions.Clear();
                CanvasGrid.ColumnDefinitions.Clear();


                switch ((int)((ScrollBar)sender).Value)
                {
                    case 2:
                        {
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition21);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition22);
                        }
                        break;
                    case 3:
                        {
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition21);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition22);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition61);
                        }
                        break;
                    case 4:
                        {
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition21);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition22);
                            CanvasGrid.RowDefinitions.Add(rowDefinition41);
                            CanvasGrid.RowDefinitions.Add(rowDefinition42);
                        }
                        break;
                    case 5:
                        {
                            CanvasGrid.RowDefinitions.Add(rowDefinition41);
                            CanvasGrid.RowDefinitions.Add(rowDefinition42);
                            CanvasGrid.RowDefinitions.Add(rowDefinition61);
                        }
                        break;
                    case 6:
                        {
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition21);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition22);
                            CanvasGrid.ColumnDefinitions.Add(columnDefinition61);
                            CanvasGrid.RowDefinitions.Add(rowDefinition41);
                            CanvasGrid.RowDefinitions.Add(rowDefinition42);
                            CanvasGrid.RowDefinitions.Add(rowDefinition61);
                        }
                        break;
                }
            }
        }
    }
}

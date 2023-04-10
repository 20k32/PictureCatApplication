using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictureCat.CustomViews
{
    /// <summary>
    /// Логика взаимодействия для AddNewCategoryCard.xaml
    /// </summary>
    public partial class AddNewCategoryCard : UserControl
    {
        Window ownerWindow = null!;
        public AddNewCategoryCard(Window OwnerWindow)
        {
            InitializeComponent();
            ownerWindow = OwnerWindow;
        }
    }
}

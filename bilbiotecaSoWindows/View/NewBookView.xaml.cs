using bilbiotecaSoWindows.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace bilbiotecaSoWindows.View
{
    /// <summary>
    /// Interaction logic for NewBookView.xaml
    /// </summary>
    public partial class NewBookView : Window
    {
        public NewBookView(ObservableCollection<Book> allBooks)
        {
            InitializeComponent();
            this.DataContext = new bilbiotecaSoWindows.ViewModel.NewBookViewModel(this, allBooks);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LibrarianView.xaml
    /// </summary>
    public partial class LibrarianView : Window
    {
        public LibrarianView()
        {
            InitializeComponent();
            this.DataContext = new bilbiotecaSoWindows.ViewModel.LibrarianViewModel(this);
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    base.OnClosing(e);

        //    if (MessageBox.Show("Sair?", "Texto", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //}
    }
}

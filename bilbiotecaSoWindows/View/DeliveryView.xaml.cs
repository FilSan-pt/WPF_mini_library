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
    /// Interaction logic for DeliveryView.xaml
    /// </summary>
    public partial class DeliveryView : Window
    {
        public DeliveryView(ObservableCollection<Reservation> reservations)
        {
            InitializeComponent();
            this.DataContext = new bilbiotecaSoWindows.ViewModel.DeliveryViewModel(this, reservations);
        }
    }
}

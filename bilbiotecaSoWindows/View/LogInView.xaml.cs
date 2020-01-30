using bilbiotecaSoWindows.DataAccess;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        public LogInView()
        {
            InitializeComponent();
            this.DataContext = new bilbiotecaSoWindows.ViewModel.LogInViewModel(this);
        }

        private void submeterLogin_Click(object sender, RoutedEventArgs e)
        {
            int role;
            DataAccessQuery query = new DataAccessQuery();

           role = query.authenticateUser(TXTusername.Text, txtPass.Password);

            if (role > 0)
            {

                switch (role)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        LibrarianView openViewLibrarian = new LibrarianView();
                        openViewLibrarian.Show();

                        this.Close();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Dados Inválidos");
            }
        }
    }
}

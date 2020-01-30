using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.DataAccess;
using bilbiotecaSoWindows.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bilbiotecaSoWindows.ViewModel
{
    public class reservationViewModel : BaseViewModel
    {
        #region constructor
        public reservationViewModel(Window reservationView, ObservableCollection<Reservation> reservations)
        {
            _reservations = reservations;
            _reservationView = reservationView;
            loadComboBox();
            saveResCommand = new Command(saveReservation);
            // objeto para 
            reservation = new Reservation();
        }
        #endregion

        #region variables
        #region private
        private Window _reservationView { get; set; }
        private Reservation _reservation { get; set; }
        // coleção para popular comboBox
        private ObservableCollection<string> _users;
        private ObservableCollection<string> _books;
        // coleção para uso na memória e notificação
        private ObservableCollection<Reservation> _reservations;
        private DataSet resultadoQuery = new DataSet();
        private DataAccessQuery query = new DataAccessQuery();
        #endregion
        #region public
        public Reservation reservation
        {
            get { return _reservation; }
            set
            {
                if (_reservation != value)
                {
                    _reservation = value;
                    NotifyOnPropertyChanged("reservation");
                }
            }
        }
        // coleção para popular comboBox
        public ObservableCollection<string> users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    NotifyOnPropertyChanged("collectionSource");
                }
            }
        }
        public ObservableCollection<string> books
        {
            get { return _books; }
            set
            {
                if (_books != value)
                {
                    _books = value;
                    NotifyOnPropertyChanged("books");
                }
            }
        }
        public Command saveResCommand { get; set; }
        #endregion
        #endregion

        #region methods
        private void loadComboBox()
        {
            resultadoQuery = query.consultDataBase("users", "");
            _users = new ObservableCollection<string>();

            foreach (DataRow row in resultadoQuery.Tables[0].Rows)
            {
                _users.Add(row[5].ToString());
            }
            //==================================================
            resultadoQuery = query.consultDataBase("books", "");
            _books = new ObservableCollection<string>();

            foreach (DataRow row in resultadoQuery.Tables[0].Rows)
            {
                _books.Add(row[0].ToString());
            }
        }
        private void saveReservation()
        {
            int saveResult = query.CreateReservationDB(reservation);

            switch(saveResult)
            {
                case 0:
                    _reservations.Add(reservation);
                    if (MessageBox.Show("Gravado com sucesso", "Confirmation") == MessageBoxResult.OK)
                    {
                        _reservationView.Close();
                    }                   
                    break;
                case 1:
                    MessageBox.Show("Livro não disponível para reserva", "Error");
                    break;
                case 2:
                    MessageBox.Show("Não foi possível gravar", "Error");
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}

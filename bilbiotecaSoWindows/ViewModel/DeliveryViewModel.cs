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
    public class DeliveryViewModel : BaseViewModel
    {
        #region variables

        #region private
        #region others
        private Reservation _reservation;
        private ObservableCollection<Reservation> _reservations;
        private Window _deliveryView;
        private string _active { get; set; }
        private string _isbnSearch { get; set; }
        private DataAccessQuery query = new DataAccessQuery();
        #endregion

        private string _completeName { get; set; }
        private string _isbnReservation { get; set; }
        private string _bookReservation { get; set; }
        private DateTime _currentDate { get; set; }
        private string _fine {get; set;}
        private string _obsReservation { get; set; }
        private string _currentObs { get; set; }
        #endregion

        #region public
        #region others
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
        public string isbnSearch
        {
            get { return _isbnSearch; }
            set
            {
                if (_isbnSearch != value)
                {
                    _isbnSearch = value;
                    NotifyOnPropertyChanged("isbnSearch");
                }
            }
        }
        public string active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    _active = value;
                    NotifyOnPropertyChanged("active");
                }
            }
        }
        public ObservableCollection<Reservation> reservations
        {
            get { return _reservations; }
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    NotifyOnPropertyChanged("reservations");
                }

            }
        }
        #endregion
        public string completeName
        {
            get { return _completeName; }
            set
            {
                if(_completeName != value)
                {
                    _completeName = value;
                    NotifyOnPropertyChanged("completeName");
                }
            }
        }
        public string isbnReservation
        {
            get { return _isbnReservation; }
            set
            {
                if (_isbnReservation != value)
                {
                    _isbnReservation = value;
                    NotifyOnPropertyChanged("isbnReservation");
                }
            }
        }
        public string bookReservation
        {
            get { return _bookReservation; }
            set
            {
                if (_bookReservation != value)
                {
                    _bookReservation = value;
                    NotifyOnPropertyChanged("bookReservation");
                }
            }
        }
        public DateTime currentDate
        {
            get { return _currentDate; }
            set
            {
                if(_currentDate != value)
                {
                    _currentDate = value;
                    NotifyOnPropertyChanged("currentDate");
                }
            }
        }
        public string fine
        {
            get { return _fine; }
            set
            {
                if(_fine != value)
                {
                    _fine = value;
                    NotifyOnPropertyChanged("fine");
                }
            }
        }
        public string obsReservation
        {
            get { return _obsReservation; }
            set
            {
                if (_obsReservation != value)
                {
                    _obsReservation = value;
                    NotifyOnPropertyChanged("obsReservation");
                }
            }
        }
        public string currentObs
        {
            get { return _currentObs; }
            set
            {
                if(_currentObs != value)
                {
                    _currentObs = value;
                    NotifyOnPropertyChanged("currentObs");
                }
            }
        }


        #region command
        public Command registerCommand { get; set; }
        public Command searchIsbnCommand { get; set; } 
        #endregion
        #endregion

        #endregion
        #region constructor
        public DeliveryViewModel(Window deliveryView, ObservableCollection<Reservation> reservations)
        {
            active = "Collapsed";
            _reservations = reservations;            
            _deliveryView = deliveryView;
            registerCommand = new Command(registerDelivery);
            searchIsbnCommand = new Command(searchIsbn);
        }        
        #endregion

        #region methods
        private void registerDelivery()
        {
            if(_reservation != null)
            {
                string dateFormat = _currentDate.ToString("MM/dd/yyyy hh:mm:ss");
                _reservation.observations = _reservation.observations + "/n" + _obsReservation;
                query.bookDelivery(_reservation, dateFormat);

                _reservations.Remove(_reservation);
            }
        }

        private void searchIsbn()
        {
            bool _found = false;
            foreach (Reservation objReservation in _reservations) 
            {
                if (objReservation.reservationBook == _isbnSearch)
                {                    
                    _found = true;
                    _reservation = objReservation;

                    _completeName = objReservation.usernameNomeProprio + " " + objReservation.usernameApelido;
                    _isbnReservation = objReservation.reservationBook;
                    _bookReservation = objReservation.bookTitle;
                    _currentDate = DateTime.Now;

                    if (objReservation.expectedDeliveryDateTime < DateTime.Now)
                    {
                        double dias = DateTime.Now.Subtract(objReservation.expectedDeliveryDateTime).TotalDays;
                        _fine = dias.ToString();
                    }
                    else
                    {
                        _fine = "0";
                    }

                    _obsReservation = objReservation.observations;

                    NotifyOnPropertyChanged("completeName");
                    NotifyOnPropertyChanged("isbnReservation");
                    NotifyOnPropertyChanged("bookReservation");
                    NotifyOnPropertyChanged("currentDate");
                    NotifyOnPropertyChanged("fine");
                    NotifyOnPropertyChanged("obsReservation");
                }
            }

            if(_found == true)
            { 
                _active = "Visible";
                NotifyOnPropertyChanged("active");
            }
        }
        #endregion
    }
}

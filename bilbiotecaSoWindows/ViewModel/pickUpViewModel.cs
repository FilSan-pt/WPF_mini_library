using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bilbiotecaSoWindows.ViewModel
{
    public class pickUpViewModel : BaseViewModel
    {
        #region variables

        #region private
        private Reservation _reservation;
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
        #endregion

        #endregion
        #region constructor
        public pickUpViewModel(Window deliveryView, ObservableCollection<Reservation> reservations)
        {

        }
        #endregion

    }
}

using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class Reservation : BaseViewModel
    {
        #region variables 
        #region public
        public string reservationId
        {
            get
            {
                return _reservationId;
            }
            set
            {
                if (_reservationId != value)
                {
                    _reservationId = value;
                    NotifyOnPropertyChanged("reservationId");
                }
            }
        }
        public string reservationUser
        {
            get
            {
                return _reservationUser;
            }
            set
            {
                if (_reservationUser != value)
                {
                    _reservationUser = value;
                    NotifyOnPropertyChanged("reservationUser");
                }
            }
        }
        public string usernameNomeProprio
        {
            get
            {
                return _usernameNomeProprio;
            }
            set
            {
                if (_usernameNomeProprio != value)
                {
                    _usernameNomeProprio = value;
                    NotifyOnPropertyChanged("usernameNomeProprio");
                }
            }
        }
        public string usernameApelido
        {
            get
            {
                return _usernameApelido;
            }
            set
            {
                if (_usernameApelido != value)
                {
                    _usernameApelido = value;
                    NotifyOnPropertyChanged("usernameApelido");
                }
            }
        }
        public string reservationBook
        {
            get
            {
                return _reservationBook;
            }
            set
            {
                if (_reservationBook != value)
                {
                    _reservationBook = value;
                    NotifyOnPropertyChanged("reservationBook");
                }
            }
        }
        public string bookTitle
        {
            get
            {
                return _bookTitle;
            }
            set
            {
                if (_bookTitle != value)
                {
                    _bookTitle = value;
                    NotifyOnPropertyChanged("bookTitle");
                }
            }
        }
        // DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind)
        // Exemplo: DateTime date1 = new DateTime(2010, 8, 18, 16, 32, 18, 500, DateTimeKind.Local);
        public DateTime reservationDateTime
        {
            get
            {
                return _reservationDateTime;
            }
            set
            {
                if (_reservationDateTime != value)
                {
                    _reservationDateTime = value;
                    NotifyOnPropertyChanged("reservationDateTime");
                }
            }
        }
        public DateTime pickUpDateTime
        {
            get
            {
                return _pickUpDateTime;
            }
            set
            {
                if (_pickUpDateTime != value)
                {
                    _pickUpDateTime = value;
                    NotifyOnPropertyChanged("pickUpDateTime");
                }
            }
        }
        public DateTime expectedDeliveryDateTime
        {
            get
            {
                return _expectedDeliveryDateTime;
            }
            set
            {
                if (_expectedDeliveryDateTime != value)
                {
                    _expectedDeliveryDateTime = value;
                    NotifyOnPropertyChanged("expectedDeliveryDateTime");
                }
            }
        }
        public DateTime deliveryDateTime
        {
            get
            {
                return _deliveryDateTime;
            }
            set
            {
                if (_deliveryDateTime != value)
                {
                    _deliveryDateTime = value;
                    NotifyOnPropertyChanged("deliveryDateTime");
                }
            }
        }
        public string observations
        {
            get
            {
                return _observations;
            }
            set
            {
                if (_observations != value)
                {
                    _observations = value;
                    NotifyOnPropertyChanged("observations");
                }
            }
        }
        #endregion

        #region private
        private string _reservationId { get; set; }
        private string _reservationUser { get; set; }
        private string _usernameNomeProprio { get; set; }
        private string _usernameApelido { get; set; }
        private string _reservationBook { get; set; }
        private string _bookTitle { get; set; }
        // DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind)
        // Exemplo: DateTime date1 = new DateTime(2010, 8, 18, 16, 32, 18, 500, DateTimeKind.Local);
        private DateTime _reservationDateTime { get; set; }
        private DateTime _pickUpDateTime { get; set; }
        private DateTime _expectedDeliveryDateTime { get; set; }
        private DateTime _deliveryDateTime { get; set; }
        private string _observations { get; set; }
        #endregion
        #endregion

        #region Constructor
        public Reservation()
        {
        }

        public Reservation(string reservationIdIn, string reservationUserIn,string usernameNomeProprioIn, string usernameApelidoIn, string reservationBookIn, string bookTitleIn, DateTime reservationDateTimeIn, DateTime pickUpDateTimeIn, DateTime expectedDeliveryDateTimeIn, DateTime deliveryDateTimeIn, string observationsIn)
        {
            reservationId = reservationIdIn;
            reservationUser = reservationUserIn;
            usernameNomeProprio = usernameNomeProprioIn;
            usernameApelido = usernameApelidoIn;
            reservationBook = reservationBookIn;
            bookTitle = bookTitleIn;
            reservationDateTime = reservationDateTimeIn;
            pickUpDateTime = pickUpDateTimeIn;
            expectedDeliveryDateTime = expectedDeliveryDateTimeIn;
            deliveryDateTime = deliveryDateTimeIn;
            observations = observationsIn;
        }
        #endregion
    }
}

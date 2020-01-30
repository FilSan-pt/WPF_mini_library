using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class User : BaseViewModel
    {
        #region variables
        #region public
        public string userGuid
        {
            get
            {
                return _userGuid;
            }
            set
            {
                if (_userGuid != value)
                {
                    _userGuid = value;
                    NotifyOnPropertyChanged("userGuid");
                }
            }
        }
        public string firstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    NotifyOnPropertyChanged("firstName");
                }
            }
        }
        public string lastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    NotifyOnPropertyChanged("lastName");
                }
            }
        }
        public string phoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    NotifyOnPropertyChanged("phoneNumber");
                }
            }
        }
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyOnPropertyChanged("email");
                }
            }
        }
        public UserLogin userLogin
        {
            get
            {
                return _userLogin;
            }
            set
            {
                if (_userLogin != value)
                {
                    _userLogin = value;
                    NotifyOnPropertyChanged("userLogin");
                }
            }
        }
        #endregion

        #region private
        private string _userGuid { get; set; }
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _phoneNumber { get; set; }
        private string _email { get; set; }
        private UserLogin _userLogin { get; set; }
        #endregion
        #endregion

        #region Constructor
        public User()
        {
            userLogin = new UserLogin();
        }

        public User(string userGuidIn, string firstNameIn, string lastNameIn, string phoneNumberIn, string emailIn, UserLogin userLoginIn)
        {
            userGuid = userGuidIn;
            firstName = firstNameIn;
            lastName = lastNameIn;
            phoneNumber = phoneNumberIn;
            email = emailIn;
            userLogin = userLoginIn;
        }
        #endregion
    }
}

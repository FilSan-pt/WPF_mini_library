using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class UserLogin : BaseViewModel
    {
        #region variables 
        public string userLoginGuid
        {
            get
            {
                return _userLoginGuid;
            }
            set
            {
                if (_userLoginGuid != value)
                {
                    _userLoginGuid = value;
                    NotifyOnPropertyChanged("userLoginGuid");
                }
            }
        }
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyOnPropertyChanged("username");
                }
            }
        }
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyOnPropertyChanged("password");
                }
            }
        }
        public UserRole userRole
        {
            get
            {
                return _userRole;
            }
            set
            {
                if (_userRole != value)
                {
                    _userRole = value;
                    NotifyOnPropertyChanged("userRole");
                }
            }
        }

        #region private
        private string _userLoginGuid { get; set; }
        private string _username { get; set; }
        private string _password { get; set; }
        private UserRole _userRole { get; set; }
        #endregion
        #endregion

        #region Constructor
        public UserLogin()
        {
            userRole = new UserRole();
        }

        public UserLogin(string userLoginGuidIn, string usernameIn, string passwordIn, UserRole userRoleIn)
        {
            userLoginGuid = userLoginGuidIn;
            username = usernameIn;
            password = passwordIn;
            userRole = userRoleIn;
        }
        #endregion
    }
}

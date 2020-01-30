using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class UserRole : BaseViewModel
    {
        #region variables 
        public int userRoleGuid
        {
            get
            {
                return _userRoleGuid;
            }
            set
            {
                if (_userRoleGuid != value)
                {
                    _userRoleGuid = value;
                    NotifyOnPropertyChanged("password");
                }
            }
        }
        public string role
        {
            get
            {
                return _role;
            }
            set
            {
                if (_role != value)
                {
                    _role = value;
                    NotifyOnPropertyChanged("role");
                }
            }
        }

        #region private
        private int _userRoleGuid { get; set; }
        private string _role { get; set; }
        #endregion
        #endregion

        #region Constructor
        public UserRole() { }

        public UserRole createUserRole(int userRoleGuidIn, string roleIn)
        {
            return new UserRole
            {
                userRoleGuid = userRoleGuidIn,
                role = roleIn
            };
        }
        #endregion
    }
}

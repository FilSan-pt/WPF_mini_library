using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.DataAccess;
using bilbiotecaSoWindows.Model;
using bilbiotecaSoWindows.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bilbiotecaSoWindows.ViewModel
{
    public class LogInViewModel : BaseViewModel
    {
        #region constructor
        public LogInViewModel(Window LogInWindow)
        {
            _LogInWindow = LogInWindow;
            logInUser = new UserLogin
            {
                username = "bibliotecario",
            };
            submitCommand = new Command(submitMethod);
        }
        #endregion

        #region properties
        #region private
        private UserLogin _logInUser { get; set; }
        private Window _LogInWindow;
        #endregion
        #region public
        public UserLogin logInUser
        {
            get { return _logInUser; }
            set
            {
                if (value != _logInUser) { _logInUser = value; NotifyOnPropertyChanged("logInUser"); }
            }
        }
        public Command submitCommand { get; set; }
        #endregion
        #endregion

        #region methods
        private void submitMethod()
        {
            int role;
            DataAccessQuery query = new DataAccessQuery();

            role = query.authenticateUser(logInUser.username, logInUser.password);

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

                        _LogInWindow.Close();
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion
    }
}

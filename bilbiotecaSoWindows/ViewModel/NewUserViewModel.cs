using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.DataAccess;
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
    public class NewUserViewModel : BaseViewModel
    {
        #region constructor
        public NewUserViewModel(Window NewUserWindow, ObservableCollection<User> allUsers)
        {
            _allUsers = allUsers;
            _NewUserWindow = NewUserWindow;
            newUser = new User();
            saveCommand = new Command(saveMethod);
        }
        #endregion

        #region properties
        #region private
        private DataAccessQuery query = new DataAccessQuery();
        private ObservableCollection<User> _allUsers; 
        private User _newUser { get; set; }
        private Window _NewUserWindow;
        #endregion

        #region public
        public User newUser
        {
            get { return _newUser; }
            set
            {
                if (value != _newUser) { _newUser = value; NotifyOnPropertyChanged("newUser"); }
            }
        }
        public Command saveCommand { get; set; }
        #endregion
        #endregion

        #region methods

        public void saveMethod()
        {
            if (query.CreateUserDB(newUser))
            {
                _allUsers.Add(newUser);
                if (MessageBox.Show("Gravado com sucesso", "Confirmation") == MessageBoxResult.OK)
                {
                    _NewUserWindow.Close();
                }                
            }
            else
            {
                MessageBox.Show("Não foi possível gravar", "Error");
            }
        }
        #endregion
    }
}

using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.DataAccess;
using bilbiotecaSoWindows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bilbiotecaSoWindows.ViewModel;
using System.Collections.ObjectModel;

namespace bilbiotecaSoWindows.ViewModel
{
    public class NewBookViewModel : BaseViewModel
    {
        #region constructor
        public NewBookViewModel(Window NewBookWindow, ObservableCollection<Book> allBooks)
        {
            _allBooks = allBooks;
            _NewBookWindow = NewBookWindow;
            book = new Book();
            newBookComand = new Command(createNewBook);
        }
        #endregion

        #region properties
        #region private
        private Book _book { get; set; }
        private DataAccessQuery query = new DataAccessQuery();
        private Window _NewBookWindow;
        private ObservableCollection<Book> _allBooks;
        #endregion

        #region
        public Book book
        {
            get { return _book; }
            set
            {
                if (value != _book) { _book = value; NotifyOnPropertyChanged("book"); }
            }
        }

        public Command newBookComand { get; set; }
        #endregion
        #endregion

        #region methods
        public void createNewBook()
        {
            if (query.CreateBookDB(book))
            {
                _allBooks.Add(book);
                if (MessageBox.Show("Gravado com sucesso", "Confirmation") == MessageBoxResult.OK)
                {
                    _NewBookWindow.Close();
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

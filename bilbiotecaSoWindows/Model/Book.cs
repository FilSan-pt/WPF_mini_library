using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class Book : BaseViewModel
    {
        #region variables 
        #region public
        public string isbn
        {
            get
            {
                return _isbn;
            }
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    NotifyOnPropertyChanged("isbn");
                }
            }
        }
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyOnPropertyChanged("title");
                }
            }
        }
        public string publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                if (_publisher != value)
                {
                    _publisher = value;
                    NotifyOnPropertyChanged("publisher");
                }
            }
        }
        public int year
        {
            get
            {
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    NotifyOnPropertyChanged("year");
                }
            }
        }
        public bool isAvailable
        {
            get
            {
                return _isAvailable;
            }
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    NotifyOnPropertyChanged("isAvailable");
                }
            }
        }
        public Author author
        {
            get
            {
                return _author;
            }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    NotifyOnPropertyChanged("author");
                }
            }
        }
        #endregion 

        #region private
        private string _isbn { get; set; }
        private string _title { get; set; }
        private string _publisher { get; set; }
        private int _year { get; set; }
        private bool _isAvailable { get; set; }
        private Author _author { get; set; }
        #endregion
        #endregion

        #region Constructor 
        public Book()
        {
            author = new Author();
        }

        public Book(string isbnIn, string titleIn, string publisherIn, int yearIn, bool isAvailableIn, Author autorIn)
        {
            isbn = isbnIn;
            title = titleIn;
            publisher = publisherIn;
            year = yearIn;
            isAvailable = isAvailableIn;
            author = autorIn;
        }
        #endregion
    }
}

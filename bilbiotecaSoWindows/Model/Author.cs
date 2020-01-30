using bilbiotecaSoWindows.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Model
{
    public class Author : BaseViewModel
    {
        #region variables
        #region public 
        public string authorGuid
        {
            get
            {
                return _authorGuid;
            }
            set
            {
                if (_authorGuid != value)
                {
                    _authorGuid = value;
                    NotifyOnPropertyChanged("authorGuid");
                }
            }
        }
        public string firstNameAuthor
        {
            get
            {
                return _firstNameAuthor;
            }
            set
            {
                if (_firstNameAuthor != value)
                {
                    _firstNameAuthor = value;
                    NotifyOnPropertyChanged("firstNameAuthor");
                }
            }
        }
        public string lastNameAuthor
        {
            get
            {
                return _lastNameAuthor;
            }
            set
            {
                if (_lastNameAuthor != value)
                {
                    _lastNameAuthor = value;
                    NotifyOnPropertyChanged("lastNameAuthor");
                }
            }
        }
        #endregion

        #region private
        private string _authorGuid { get; set; }
        private string _firstNameAuthor { get; set; }
        private string _lastNameAuthor { get; set; }
        #endregion        
        #endregion

        #region Constructor
        public Author() { }

        public Author(string authorGuidIn, string firstNameAuthorIn, string lastNameAuthorIn)
        {
            authorGuid = authorGuidIn;
            firstNameAuthor = firstNameAuthorIn;
            lastNameAuthor = lastNameAuthorIn;
        }
        #endregion
    }
}

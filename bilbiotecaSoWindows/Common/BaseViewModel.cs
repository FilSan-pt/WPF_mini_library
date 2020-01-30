using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Common
{
    public class BaseViewModel : Disposable, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region constructor

        protected BaseViewModel() { }

        #endregion

        #region INotifyPropertyChanged

        #region properties
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
        #endregion

        //-----------------------------------------------------------------------------

        #region methods
        // lança o aviso de mudança da propriedade	
        protected virtual void NotifyOnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            //recebe as carateristicas de um determinado componente, se conseguir aceder à propriedade é porque a propriedade existe
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        //-----------------------------------------------------------------------------	

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion

        #region INotifyDataErrorInfo

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // 
        public IEnumerable GetErrors(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
                return this._errors[propertyName];
            return null;
        }

        // boleano para indicar se tem erros 
        public bool HasErrors
        {
            get { return (this._errors.Count > 0); }
        }

        // bolenao que indica que objeto não tem erros e é válido
        public bool IsValid
        {
            get { return !this.HasErrors; }

        }

        // adicionar erros à lista
        public void AddError(string propertyName, string error)
        {
            this._errors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
                this._errors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        #region interfaceParaIdDaView
        public interface IRequireViewIdentification
        {
            Guid viewGuid { get; }
        }
        #endregion

    }
}

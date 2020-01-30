using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilbiotecaSoWindows.Common
{
    public class Disposable
    {
        #region variáveis

        //private bool isDisposed;
        //******* ver esta parte
        //private Subject<unit> whenDisposedSubject;

        // quando o objeto ou propriedade é "destruído" devolve os erros aquando desse evento GET
        //public IObservable <Unit> WhenDisposed
        //{
        //    get
        //    {
        //        if (this.IsDispose)
        //        {
        //            return Observable.Return(Unit.Default);
        //        }
        //        else
        //        {
        //            if (this.whenDisposedSubject == null)
        //            {
        //                this.whenDisposedSubject = new Subject<Unit>();
        //            }

        //            return this.whenDisposedSubject.AsObservable();
        //        }
        //    }
        //}

        // booleano que devolve se foi "destruído" GET
        //public bool IsDispose
        //{
        //    //get { return this.IsDispose; }
        //}

        #endregion

        #region destructor

        ~Disposable()
        {
            //método bool mais à frente criado, vai existir um public para ser usado em OverRide e o private que faz o Dispose;
            this.Dispose(false);
        }

        #endregion

        #region métodos públicos possibilidade OverRide

        public void Dispose()
        {
            //faz o dispose de tudo porque entra a true
            this.Dispose(true);

            //GC reclama memória por usar e impede a execução do objeto após dispose
            GC.SuppressFinalize(this);
        }

        // pode ser usado pelos herdeiros para overRide
        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnManaged()
        {
        }

        protected void ThrowIfDisposed()
        {
            //if (this.isDisposed)
            //{
            //    throw new ObjectDisposedException(this.GetType().Name);
            //}
        }

        #endregion

        #region métodos exclusivos da classe 

        // se true liberta tudo, se falso só liberta os recursos invocados mas não usados
        private void Dispose(bool disposing)
        {
            //se ainda não existe uma invocação do método
            //if (!this.IsDispose)
            //{
            //    if (disposing)
            //    {
            //        this.DisposeManaged();
            //    }

            //    // invoca o método vazio dos recursos que não são usados
            //    this.DisposeUnManaged();

            //    // foi feita a "destruição"
            //    this.isDisposed = true;

            //    //if (this.whenDisposedSubject != null)
            //    //{
            //    //    // Raise the WhenDisposed event.
            //    //    this.whenDisposedSubject.OnNext(Unit.Default);
            //    //    this.whenDisposedSubject.OnCompleted();
            //    //    this.whenDisposedSubject.Dispose();
            //    //}

            //}
        }

        #endregion
    }
}

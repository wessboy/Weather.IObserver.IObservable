using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.observer.Data;

namespace weather.observer.ObserverPattern;
      internal sealed class Unsbuscriber : IDisposable
    {
      private List<IObserver<Temperature>> _observers;
      private IObserver<Temperature> _observer;

      public Unsbuscriber(List<IObserver<Temperature>> observers , IObserver<Temperature> observer)
       {
        this._observers = observers;
        this._observer = observer;
        
       }

        public void Dispose()
    {
        if (!(_observer == null))
            _observers.Remove(_observer);
    }

}


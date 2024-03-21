using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.observer.Data;

namespace weather.observer.ObserverPattern;

    public class TemperatureReporter : IObserver<Temperature>
    {
        private IDisposable unsubscriber;
        private bool first = true;
        private Temperature last;

   

    public virtual void Subscribe(IObservable<Temperature> provider) 
         {
        
            unsubscriber = provider.Subscribe(this);
         }

      public virtual void Unsubscribe()
       {
         unsubscriber.Dispose();
       }


    public void OnCompleted()
    {
        Console.WriteLine("Additional temperature data will not be transmitted.");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("couldn't fetch data from sensor !!");
    }

    public void OnNext(Temperature value)
    {
        Console.WriteLine("The temperature is {0}° at {1:g}",value.Mesure,value.MesuredAt);
       if (first)
        {
            last = value;
            first = false;
        }
        else
        {
            Console.WriteLine("  change: {0}° in {1:g}",value.Mesure - last.Mesure,value.MesuredAt.ToUniversalTime() - last.MesuredAt.ToUniversalTime());
        }
    }

}


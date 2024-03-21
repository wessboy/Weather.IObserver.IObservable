using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.observer.Data;

namespace weather.observer.ObserverPattern
{
     public class TemperatureMonitor : IObservable<Temperature>
    {
        List<IObserver<Temperature>> observers;

        public TemperatureMonitor()
        {
            observers = new List<IObserver<Temperature>>();
        }

        public IDisposable Subscribe(IObserver<Temperature> observer)
        {
            if(! observers.Contains(observer))
                observers.Add(observer);

            return new Unsbuscriber(observers,observer);
        }


        public void GetTemperature()
        {
            Nullable<Decimal>[] temperatures = { 14.6m, 14.65m, 14.7m, 14.9m, 14.9m, 15.2m, 15.25m, 15.2m, 15.4m, 15.45m, null };

            Nullable<Decimal> previousTemperature = null;
            bool start = true;

            foreach(var temperature in temperatures)
            {
                System.Threading.Thread.Sleep(2500);
                if(temperature.HasValue)
                {
                    if(start || (Math.Abs(temperature.Value - previousTemperature.Value) >= 0.1m)) 
                    {
                        Temperature newMesure = new Temperature { Mesure = temperature.Value,MesuredAt = DateTime.Now};

                        foreach(var observer in observers)
                            observer.OnNext(newMesure);
                        previousTemperature = temperature;
                        if(start)
                            start = false;

                    }
                }

                else
                {
                    foreach(var observer in observers.ToArray())
                        if(observer != null)
                             observer.OnCompleted();
                    observers.Clear();
                    break;
                }
            }
        }

    }
}

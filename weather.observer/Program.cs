
using weather.observer.ObserverPattern;

TemperatureMonitor temperatureMonitor = new();
TemperatureReporter temperatureReporter = new();
temperatureReporter.Subscribe(temperatureMonitor);
temperatureMonitor.GetTemperature();

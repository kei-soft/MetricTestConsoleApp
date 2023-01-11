using System.Diagnostics.Metrics;

using OpenTelemetry;
using OpenTelemetry.Metrics;

class Program
{
    static Meter meter = new Meter("HatStore", "1.0.0");
    static Counter<int> hatsSold = meter.CreateCounter<int>(name: "hats-sold",
                                                            unit: "Hats",
                                                            description: "The number of hats sold in our store");
    static void Main(string[] args)
    {
        using MeterProvider meterProvider = Sdk.CreateMeterProviderBuilder()
               .AddMeter("HatStore")
               .AddPrometheusExporter(opt =>
               {
                   opt.StartHttpListener = true;
                   opt.HttpListenerPrefixes = new string[] { $"http://localhost:9184/" };
               })
               .Build();

        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has a transaction each second that sells 4 hats
            Thread.Sleep(1000);
            Console.WriteLine($"{DateTime.Now.ToString()} : 4 {hatsSold.Name} ({hatsSold.Meter.Name})");
            hatsSold.Add(4);
        }
    }
}
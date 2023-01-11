using System.Diagnostics.Metrics;
class Program
{
    static Meter meter = new Meter("HatStore", "1.0.0");
    static Counter<int> hatsSold = meter.CreateCounter<int>("hats-sold");

    static void Main(string[] args)
    {
        Console.WriteLine("Press any key to exit");
        while (!Console.KeyAvailable)
        {
            // Pretend our store has a transaction each second that sells 4 hats
            Thread.Sleep(1000);
            hatsSold.Add(4);
        }
    }
}
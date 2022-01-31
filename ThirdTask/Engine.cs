namespace ThirdTask
{
    public class Engine
    {
        public int Power { get; }

        public double Volume { get; }

        public EngineType Type { get; }

        public string SerialNumber { get; }

        public Engine(int power, double volume, EngineType type, string serialNumber)
        {
            Power = power;
            Volume = volume;
            Type = type;
            SerialNumber = serialNumber;
        }

        public override string ToString() => $"{Power}\n{Volume}\n{Type}\n{SerialNumber}\n";
    }
}
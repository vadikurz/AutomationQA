namespace ThirdTask
{
    public class Engine
    {
        public int Power { get; set; }

        public double Volume { get; set; }

        public EngineType Type { get; set; }

        public string SerialNumber { get; set; }

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

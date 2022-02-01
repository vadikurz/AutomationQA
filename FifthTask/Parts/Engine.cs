namespace FifthTask.Parts
{
    public class Engine
    {
        public int Power { get; set; }

        public double Capacity { get; set; }

        public EngineType Type { get; set; }

        public string SerialNumber { get; set; }

        public Engine(){}
        public Engine(int power, double capacity, EngineType type, string serialNumber)
        {
            Power = power;
            Capacity = capacity;
            Type = type;
            SerialNumber = serialNumber;
        }

        public override string ToString()
        {
            return $"{Power}\n{Capacity}\n{Type}\n{SerialNumber}\n";
        }
    }
}

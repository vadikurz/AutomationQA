namespace FifthTask
{
    public class Transmission
    {
        public TransmissionType Type { get; set; }

        public ushort NumberOfGears { get; set; }

        public string Manufacturer { get; set; }

        private Transmission(){}
        public Transmission(TransmissionType type, ushort numberOfGears, string manufacturer)
        {
            Type = type;
            NumberOfGears = numberOfGears;
            Manufacturer = manufacturer;
        }

        public override string ToString()
        {
            return $"{Type}\n{NumberOfGears}\n{Manufacturer}\n";
        }
    }
}

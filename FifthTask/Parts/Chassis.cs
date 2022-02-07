namespace FifthTask
{
    public class Chassis
    {
        public ushort NumberOfWheels { get; set; }

        public string SerialNumber { get; set; }

        public ushort PermissibleLoad { get; set; }

        private Chassis(){}

        public Chassis(ushort numberOfWheels, string serialNumber, ushort permissibleLoad)
        {
            NumberOfWheels = numberOfWheels;
            SerialNumber = serialNumber;
            PermissibleLoad = permissibleLoad;
        }

        public override string ToString() => $"{NumberOfWheels}\n{SerialNumber}\n{PermissibleLoad}\n";
    }
}

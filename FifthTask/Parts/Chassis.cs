namespace FifthTask
{
    public class Chassis
    {
        public ushort NumberOfWheels { get; set; }

        public string SerialNumber { get; set; }

        public ushort PermissibleLoad { get; set; }

        public Chassis(){}
        public Chassis(ushort numberOfWheels, string serialNumber, ushort permissibleLoad)
        {
            NumberOfWheels = numberOfWheels;
            SerialNumber = serialNumber;
            PermissibleLoad = permissibleLoad;
        }
    }
}

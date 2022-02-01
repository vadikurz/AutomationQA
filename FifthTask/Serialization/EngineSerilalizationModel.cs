using System;

namespace FifthTask.Serialization
{
    [Serializable]
    public class EngineSerializationModel
    {
        public int Power { get; set; }

        public EngineType Type { get; set; }

        public string SerialNumber { get; set; }

        public EngineSerializationModel() { }
        public EngineSerializationModel(int power, EngineType type, string serialNumber)
        {
            Power = power;
            Type = type;
            SerialNumber = serialNumber;
        }
    }
}

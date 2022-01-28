using System;

namespace FifthTask
{
    [Serializable]
    public class EngineSerilalizationModel
    {
        public int Power { get; set; }

        public EngineType Type { get; set; }

        public string SerialNumber { get; set; }

        public EngineSerilalizationModel() { }
        public EngineSerilalizationModel(int power, EngineType type, string serialNumber)
        {
            Power = power;
            Type = type;
            SerialNumber = serialNumber;
        }
    }
}

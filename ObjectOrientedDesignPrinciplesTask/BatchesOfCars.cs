namespace ObjectOrientedDesignPrinciplesTask
{
    public struct BatchOfCars
    {
        public string Type { get; set; }

        public string Model { get; set; }

        public int Number { get; set; }

        public double Price { get; set; }

        public BatchOfCars(string type, string model, int number, double price)
        {
            Type = type;
            Model = model;
            Number = number;
            Price = price;
        }
    }
}

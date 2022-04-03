using System;
using System.Collections.Generic;

namespace ObjectOrientedDesignPrinciplesTask
{
    public class CarPark
    {
        public List<BatchOfCars> BatchesOfCars { get; set; }

        public CarPark()
        {
            BatchesOfCars = new List<BatchOfCars>();
        }
    }
}

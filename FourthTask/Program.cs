using System;
using System.Collections.Generic;

namespace FourthTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var point = new Point(15, 2, 3);

                var Flyable = new List<IFlyable>();

                Flyable.Add(new Bird(point));
                Flyable.Add(new Airplane(point));
                Flyable.Add(new Drone(point, 50));

                foreach (var flyable in Flyable)
                {
                    flyable.FlyTo(new Point(75, 68, 45));
                    flyable.GetFlyTime(new Point(123, 150, 75));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

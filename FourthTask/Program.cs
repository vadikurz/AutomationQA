using System;

namespace FourthTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var point = new Point(15, 2, 3);
                Bird bird = new Bird(point);
                bird.FlyTo(new Point(100, 20, 35));
                Console.WriteLine(bird.CurrentPoint);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

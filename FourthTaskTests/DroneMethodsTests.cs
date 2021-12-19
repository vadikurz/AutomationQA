using FourthTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourthTaskTests
{
    [TestClass]
    public class DroneMethodsTests
    {
        [TestMethod]
        [DataRow(9.1366076855, new[] { 15, 2, 3 }, new[] { 400, 232, 90 }, 50, 1e-6)]
        public void GetFlyTimeTestPositive(double expectedTime, int[] currentPoint, int[] destinationPoint, int speed, double eps)
        {
            var drone = new Drone(new Point(currentPoint[0], currentPoint[1], currentPoint[2]), speed);

            double actualTime = drone.GetFlyTime(new Point(destinationPoint[0], destinationPoint[1], destinationPoint[2]));

            Assert.AreEqual(expectedTime, actualTime, eps);
        }
    }
}

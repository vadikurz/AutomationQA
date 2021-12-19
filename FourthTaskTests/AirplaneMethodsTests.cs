using FourthTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourthTaskTests
{
    [TestClass]
    public class AirplaneMethodsTests
    {
        [TestMethod]
        [DataRow(0.7510423123, new[] { 15, 2, 3 }, new[] { 100, 200, 38 })]
        public void GetFlyTimeTestPositive(double expectedTime, int[] currentPoint, int[] destinationPoint)
        {
            var airplane = new Airplane(new Point(currentPoint[0], currentPoint[1], currentPoint[2]));

            double actualTime = airplane.GetFlyTime(new Point(destinationPoint[0], destinationPoint[1], destinationPoint[2]));

            Assert.AreEqual(expectedTime, actualTime, 0.000001);
        }
    }
}

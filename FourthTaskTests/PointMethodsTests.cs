using FourthTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourthTaskTests
{
    [TestClass]
    public class PointMethodsTests
    {
        [TestMethod]
        [DataRow(792.13950791, new[] { 17, 3, 5 }, new[] { 143, 256, 745 }, 1e-6)]
        public void GetDistanceTestPositive(double expectedDistance, int[] startingPoint, int[] destination, double eps)
        {
            var startPoint = new Point(startingPoint[0], startingPoint[1], startingPoint[2]);
            var destinationPoint = new Point(destination[0], destination[1], destination[2]);

            double actualDistance = startPoint.GetDistance(startPoint, destinationPoint);

            Assert.AreEqual(expectedDistance, actualDistance, eps);
        }

    }
}

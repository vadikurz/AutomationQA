using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecondTask;

namespace SecondTaskTests
{
    [TestClass]
    public class NumberConverterTests
    {
        [TestMethod]
        public void TestConvertPositive()
        {
            var converter = new NumberConverter();
            Assert.AreEqual("-10011", converter.Convert(-19,2));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecondTask;

namespace SecondTaskTests
{
    public record TestCase(string NumberToConvert, int Base, int ConverationResult);

    [TestClass]
    public class NumberConverterTests
    {

        [TestMethod]
        [DataRow(new TestCase("-10011", 2, -19))]
        public void TestConvertPositive()
        {
            var converter = new NumberConverter();
            Assert.AreEqual("-10011", converter.Convert(-19,2));
        }
    }
}

using System;
using FirstTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstTaskTests
{
    [TestClass]
    public class MaxCountOfUniqueSequentialCharactersTests
    {
        [TestMethod]
        [DataRow(4, "abcdab")]
        [DataRow(0, "")]
        [DataRow(2, "aba")]
        [DataRow(1, "aa")]
        public void GivenNormalStringShouldBePositive(int expected, string testString)
        {
            int actualNumber = testString.MaxCountOfUniqueSequentialCharacters();

            Assert.AreEqual(expected, actualNumber);
        }

        [TestMethod]
        public void GivenNullShouldThrowException()
        {
            var testString = default(string);
            Assert.ThrowsException<ArgumentNullException>(() => testString.MaxCountOfUniqueSequentialCharacters());
        }
    }
}

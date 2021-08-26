using NUnit.Framework;
using MarkemViewer_CommandSender;

namespace T_MarkemViewer_CommandSender
{
    [TestFixture]
    public class Tests
    {
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("10")]
        [TestCase("6400000000")]
        public void ValidateNumer_WhenPositiveNumberIsGiven_ReturnsTrue(string numberToValidate)
        {
            Assert.IsTrue(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("-10")]
        public void ValidateNumer_WhenNegativeNumberIsGiven_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("kkk")]
        [TestCase("123kkK")]
        public void ValidateNumer_WhenNumberIsMixedWithText_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("kkk")]
        public void ValidateNumer_WhenNumberIsText_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("012")]
        public void ValidateNumer_WhenNumberContainsZeroOnTheBegining_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("0 2")]
        public void ValidateNumer_WhenNumberContainsWhiteSpacesAndNumbers_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
        [TestCase("")]
        public void ValidateNumer_WhenNumberIsEmpty_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }

        [TestCase("    ")]
        public void ValidateNumer_WhenNumberContainsWhiteSpaces_ReturnsFalse(string numberToValidate)
        {
            Assert.IsFalse(numberToValidate.ValidatePositiveNumber());
        }
    }
}
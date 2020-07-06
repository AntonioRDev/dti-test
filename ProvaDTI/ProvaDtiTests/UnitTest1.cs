using NUnit.Framework;
using ProvaDTI.Util;

namespace ProvaDtiTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckValidString()
        {
            var invalid = Validator.IsEmptyString("");
            var valid = Validator.IsEmptyString("abc");

            Assert.AreEqual(invalid, true);
            Assert.AreEqual(valid, false);
        }

        [Test]
        public void CheckValidEmail()
        {
            var invalid = Validator.IsValidEmail("antonioribdevgmail.com");
            var valid = Validator.IsValidEmail("antonioribdev@gmail.com");

            Assert.AreEqual(invalid, false);
            Assert.AreEqual(valid, true);
        }

        [Test]
        public void CheckValidDate()
        {
            var invalid = Validator.IsValidDate("04091999");
            var valid = Validator.IsValidDate("04/09/1999");

            Assert.AreEqual(invalid, false);
            Assert.AreEqual(valid, true);
        }

        [Test]
        public void CheckValidDoubleConversion()
        {
            var invalid = Validator.IsValidDoubleConversion("abc");
            var valid = Validator.IsValidDoubleConversion("12,5");

            Assert.AreEqual(invalid, false);
            Assert.AreEqual(valid, true);
        }

        [Test]
        public void CheckValidIntegerConversion()
        {
            var invalid = Validator.IsValidIntegerConversion("abc");
            var valid = Validator.IsValidIntegerConversion("1234");

            Assert.AreEqual(invalid, false);
            Assert.AreEqual(valid, true);
        }
    }
}
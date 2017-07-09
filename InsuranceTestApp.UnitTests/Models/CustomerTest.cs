using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceTestApp.Models;
using InsuranceTestApp.UnitTests.Utilities;

namespace InsuranceTestApp.UnitTests.Models
{
    /// <summary>
    /// Contains tests for the <see cref="Customer"/> class.
    /// </summary>
    [TestClass]
    public class CustomerTests
    {
        /// <summary>
        /// Creates the unit under test, with valid values.
        /// </summary>
        /// <returns>A valid model.</returns>
        private Customer CreateUnitUnderTest()
        {
            return new Customer()
            {
                Id = 1,
                MailingAddress = "123 Test Rd.",
                City = "Test City",
                Name = "Test Name",
                State = "Test State",
                ZipCode = "12345",
            };
        }

        [TestMethod]
        public void TestValidModelHasNoModelErrors()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 0);
        }

        [TestMethod]
        public void TestNameRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.Name = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestNameLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.Name = TestUtilities.LongInvalidTestString;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestMailingAddressRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.MailingAddress = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestMailingAddressLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.MailingAddress = TestUtilities.LongInvalidTestString;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestCityRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.City = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestCityLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.City = TestUtilities.LongInvalidTestString;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestStateRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.State = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestStateLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.State = TestUtilities.LongInvalidTestString;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestZipCodeRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.ZipCode = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestValidNineDigitZipCode()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.ZipCode = "12345-6789";

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 0);
        }

        [TestMethod]
        public void TestInvalidZipCode()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.ZipCode = "123456";

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }
    }
}

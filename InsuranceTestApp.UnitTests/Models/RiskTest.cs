using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceTestApp.Enums;
using InsuranceTestApp.Models;
using InsuranceTestApp.UnitTests.Utilities;

namespace InsuranceTestApp.UnitTests.Models
{
    /// <summary>
    /// Contains tests for the <see cref="Risk"/> class.
    /// </summary>
    [TestClass]
    public class RiskTests
    {
        /// <summary>
        /// Creates the unit under test, with valid values.
        /// </summary>
        /// <returns>A valid model.</returns>
        private Risk CreateUnitUnderTest()
        {
            return new Risk()
            {
                Id = 1,
                ConstructionType = RiskConstructionType.SiteBuiltHome,
                YearBuilt = DateTime.Now.Year.ToString(),
                Address = "123 Test Rd.",
                City = "Test City",
                State = "Test State",
                ZipCode = "12345",
            };
        }

        [TestMethod]
        public void TestValidViewModelHasNoModelErrors()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 0);
        }

        [TestMethod]
        public void TestInvalidConstructionType()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.ConstructionType = RiskConstructionType.SiteBuiltHome + 100;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestYearBuiltRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.YearBuilt = null;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestInvalidYearBuilt()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.YearBuilt = "11";

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestAddressRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.Address = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestAddressLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.Address = TestUtilities.LongInvalidTestString;

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

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceTestApp.UnitTests.Utilities;
using InsuranceTestApp.ViewModels;

namespace InsuranceTestApp.UnitTests.ViewModels
{
    /// <summary>
    /// Contains tests for the <see cref="PolicyViewModel"/> class.
    /// </summary>
    [TestClass]
    public class PolicyViewModelTests
    {
        /// <summary>
        /// Creates the unit under test, with valid values.
        /// </summary>
        /// <returns>A valid view model.</returns>
        private PolicyViewModel CreateUnitUnderTest()
        {
            return new PolicyViewModel()
            {
                Id = 1,
                PolicyNumber = "1234",
                EffectiveDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                PrimaryInsured = new CustomerViewModel(),
                Risk = new RiskViewModel(),
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
        public void TestPolicyNumberRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.PolicyNumber = string.Empty;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestPolicyNumberLimit()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.PolicyNumber = TestUtilities.LongInvalidTestString;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestEffectiveDateRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.EffectiveDate = null;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }

        [TestMethod]
        public void TestExpirationDateRequired()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            uut.ExpirationDate = null;

            // Test
            var validationResult = TestUtilities.ValidateModel(uut);

            // Verify
            Assert.IsTrue(validationResult.Count == 1);
        }
    }
}

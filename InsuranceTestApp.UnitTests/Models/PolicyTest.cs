using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceTestApp.Models;
using InsuranceTestApp.UnitTests.Utilities;

namespace InsuranceTestApp.UnitTests.Models
{
    /// <summary>
    /// Contains tests for the <see cref="Policy"/> class.
    /// </summary>
    [TestClass]
    public class PolicyTests
    {
        /// <summary>
        /// Creates the unit under test, with valid values.
        /// </summary>
        /// <returns>A valid model.</returns>
        private Policy CreateUnitUnderTest()
        {
            return new Policy()
            {
                Id = 1,
                PrimaryInsuredId = 1,
                RiskId = 1,
                PolicyNumber = "1234",
                EffectiveDate = DateTime.Now,
                ExpirationDate = DateTime.Now,
                PrimaryInsured = new Customer(),
                Risk = new Risk(),
            };
        }

        [TestMethod]
        public void TestValidModelHasNoValidationErrors()
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
    }
}
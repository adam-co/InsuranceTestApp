using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InsuranceTestApp.Controllers;
using InsuranceTestApp.Enums;
using InsuranceTestApp.Models;
using InsuranceTestApp.Repositories;
using InsuranceTestApp.UnitTests.Utilities;
using InsuranceTestApp.Utilities;
using InsuranceTestApp.ViewModels;

namespace InsuranceTestApp.UnitTests.Controllers
{
    /// <summary>
    /// Contains tests for the <see cref="HomeController"/> class.
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        /// Mock unit of work used for testing.
        /// </summary>
        private Mock<IUnitOfWork> mockUnitOfWork;
        /// <summary>
        /// Mock repository used for testing.
        /// </summary>
        private Mock<IRepository<Policy>> mockRepository;

        /// <summary>
        /// Creates the unit under test.
        /// </summary>
        /// <returns>A new class testing.</returns>
        private HomeController CreateUnitUnderTest()
        {
            return new HomeController(mockUnitOfWork.Object);
        }

        /// <summary>
        /// Creates a valid <see cref="PolicyViewModel"/> that can be used for testing.
        /// </summary>
        /// <returns>A valid <see cref="PolicyViewModel"/> that can be used for testing.</returns>
        private PolicyViewModel CreateTestPolicyViewModel()
        {
            return new PolicyViewModel()
            {
                Id = 1,
                PolicyNumber = "TestPolicyNumber",
                EffectiveDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                PrimaryInsured = new CustomerViewModel()
                {
                    Id = 1,
                    MailingAddress = "123 Test Rd.",
                    City = "Test City",
                    Name = "Test Name",
                    State = "Test State",
                    ZipCode = "12345",
                },
                Risk = new RiskViewModel()
                {
                    Id = 1,
                    ConstructionType = RiskConstructionType.SiteBuiltHome,
                    YearBuilt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Address = "123 Test Rd.",
                    City = "Test City",
                    State = "Test State",
                    ZipCode = "12345",
                },
            };
        }

        /// <summary>
        /// Verifies that the specified JSON result contains the specified error message.
        /// </summary>
        /// <param name="jsonResult">JSON result to check.</param>
        /// <param name="errorMessage">Error message to check.</param>
        private void AssertThatJsonResultContainsErrorMessage(JsonResult jsonResult, string errorMessage)
        {
            Assert.IsNotNull(jsonResult);
            Assert.IsNotNull(jsonResult.Data);
            Assert.IsTrue(jsonResult.Data.GetReflectedProperty("Result").ToString() == "ERROR");
            Assert.IsTrue(jsonResult.Data.GetReflectedProperty("Message").ToString().Contains(errorMessage));
        }

        /// <summary>
        /// Used to re-initialize variables between tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepository = new Mock<IRepository<Policy>>();

            mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Policy>()).Returns(mockRepository.Object);
        }

        [TestMethod]
        public void TestIndexActionReturnsViewResult()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            var viewResult = uut.Index() as ViewResult;

            // Verify
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void TestPolicyListReturnsPolicies()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testPolicy = new Policy()
            {
                PolicyNumber = "TestPolicyNumber",
                PrimaryInsured = new Customer(),
                Risk = new Risk(),
            };
            var testQueryable = new List<Policy>() { testPolicy }.AsQueryable();
            mockRepository.Setup(repo => repo.GetAll(null)).Returns(testQueryable);

            // Test
            var jsonResult = uut.PolicyList();

            // Verify
            Assert.IsNotNull(jsonResult);
            Assert.IsNotNull(jsonResult.Data);
            Assert.IsTrue(jsonResult.Data.GetReflectedProperty("Result").ToString() == "OK");
            Assert.IsTrue((int)jsonResult.Data.GetReflectedProperty("TotalRecordCount") == 1);
            Assert.IsTrue(
                ((List<PolicyViewModel>)jsonResult.Data.GetReflectedProperty("Records"))
                .Any(viewModel => viewModel.PolicyNumber == testPolicy.PolicyNumber));
        }

        [TestMethod]
        public void TestPolicyListHandlesException()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            var testException = new Exception("test exception");
            mockRepository.Setup(repo => repo.GetAll(null)).Throws(testException);

            // Test
            var jsonResult = uut.PolicyList();

            // Verify
            AssertThatJsonResultContainsErrorMessage(jsonResult, testException.Message);
        }

        [TestMethod]
        public void TestGetRiskConstructionTypesReturnsRiskContructionTypes()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            var jsonResult = uut.GetRiskConstructionTypes();

            // Verify
            Assert.IsNotNull(jsonResult);
            Assert.IsNotNull(jsonResult.Data);
            Assert.IsTrue(jsonResult.Data.GetReflectedProperty("Result").ToString() == "OK");
            Assert.IsTrue(
                ((IEnumerable<dynamic>)
                    jsonResult.Data.GetReflectedProperty("Options")).Count() == 4);
        }

        [TestMethod]
        public void TestCreatePolicyViewModelAddsPolicyToRepo()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testPolicyViewModel = CreateTestPolicyViewModel();

            // Test
            var jsonResult = uut.CreatePolicy(testPolicyViewModel);

            // Verify
            mockRepository.Verify(
                repo => repo.Add(It.Is<Policy>(policy => policy.PolicyNumber == testPolicyViewModel.PolicyNumber)),
                Times.Once());

            Assert.IsNotNull(jsonResult);
            Assert.IsNotNull(jsonResult.Data);
            Assert.IsTrue(jsonResult.Data.GetReflectedProperty("Result").ToString() == "OK");
            Assert.IsTrue(
                ((PolicyViewModel) jsonResult.Data.GetReflectedProperty("Record"))
                .PolicyNumber == testPolicyViewModel.PolicyNumber);
        }

        [TestMethod]
        public void TestCreatePolicyHandlesGenericException()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testPolicyViewModel = CreateTestPolicyViewModel();

            var testException = new Exception("test exception");
            mockRepository.Setup(repo => repo.Add(It.IsAny<Policy>())).Throws(testException);

            // Test
            var jsonResult = uut.CreatePolicy(testPolicyViewModel);

            // Verify
            AssertThatJsonResultContainsErrorMessage(jsonResult, testException.Message);
        }

        [TestMethod]
        public void TestCreatePolicyHandlesDbEntityValidationException()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testPolicyViewModel = CreateTestPolicyViewModel();

            var testException = new DbEntityValidationException();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Policy>())).Throws(testException);

            // Test
            var jsonResult = uut.CreatePolicy(testPolicyViewModel);

            // Verify
            AssertThatJsonResultContainsErrorMessage(jsonResult, "Invalid data received");
        }

        [TestMethod]
        public void TestCreatePolicyHandlesInvalidModelState()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testPolicyViewModel = CreateTestPolicyViewModel();

            var modelErrorMessage = "invalid test error";
            uut.ModelState.AddModelError("test", modelErrorMessage);

            // Test
            var jsonResult = uut.CreatePolicy(testPolicyViewModel);

            // Verify
            AssertThatJsonResultContainsErrorMessage(jsonResult, "Invalid form entry");
            AssertThatJsonResultContainsErrorMessage(jsonResult, modelErrorMessage);
        }
    }
}

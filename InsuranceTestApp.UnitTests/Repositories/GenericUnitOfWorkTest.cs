using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InsuranceTestApp.Models;
using InsuranceTestApp.Repositories;

namespace InsuranceTestApp.UnitTests.Repositories
{
    /// <summary>
    /// Contains tests for the <see cref="GenericUnitOfWork"/> class.
    /// </summary>
    [TestClass]
    public class GenericUnitOfWorkTest
    {
        /// <summary>
        /// Mock database context used for testing.
        /// </summary>
        private Mock<DbContext> mockDbContext;

        /// <summary>
        /// Creates the unit under test.
        /// </summary>
        /// <returns>A new class testing.</returns>
        private GenericUnitOfWork CreateUnitUnderTest()
        {
            return new GenericUnitOfWork(mockDbContext.Object);
        }

        /// <summary>
        /// Used to re-initialize variables between tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockDbContext = new Mock<DbContext>();
        }

        [TestMethod]
        public void TestThatUnitOfWorkCreatesNewRepository()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            Assert.IsTrue(uut.Repositories.Count == 0);
            var policyRepo = uut.Repository<Policy>();

            // Verify
            Assert.IsTrue(uut.Repositories.Count == 1);
            Assert.IsTrue(policyRepo is GenericRepository<Policy>);
        }

        [TestMethod]
        public void TestThatUnitOfWorkReturnsSameRepository()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            var policyRepo = uut.Repository<Policy>();
            var policyRepo2 = uut.Repository<Policy>();

            // Verify
            Assert.AreEqual(policyRepo, policyRepo2);
        }

        [TestMethod]
        public void TestThatSaveChangesSavesToDbContext()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test
            uut.SaveChanges();

            // Verify
            mockDbContext.Verify(dbContext => dbContext.SaveChanges(), Times.Once());
        }
    }
}

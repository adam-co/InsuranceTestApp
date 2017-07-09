using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InsuranceTestApp.Models;
using InsuranceTestApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InsuranceTestApp.UnitTests.Repositories
{
    /// <summary>
    /// Contains tests for the <see cref="GenericRepository"/> class.
    /// </summary>
    [TestClass]
    public class GenericRepositoryTest
    {
        /// <summary>
        /// Mock database context used for testing.
        /// </summary>
        private Mock<DbContext> mockDbContext;

        /// <summary>
        /// Mock database set used for testing.
        /// </summary>
        private Mock<DbSet<Policy>> mockDbSet;

        /// <summary>
        /// Creates the unit under test.
        /// </summary>
        /// <returns>A new class testing.</returns>
        private GenericRepository<Policy> CreateUnitUnderTest()
        {
            return new GenericRepository<Policy>(mockDbContext.Object);
        }

        /// <summary>
        /// Used to re-initialize variables between tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockDbContext = new Mock<DbContext>();
            mockDbSet = new Mock<DbSet<Policy>>();
            mockDbSet.As<IQueryable<Policy>>();
            mockDbContext.Setup(dbContext => dbContext.Set<Policy>()).Returns(mockDbSet.Object);
        }
        
        [TestMethod]
        public void TestGetAllReturnsAllObjectsInDbContext()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var returnQueryable = new List<Policy>() { new Policy() }.AsQueryable();
            mockDbSet.As<IQueryable<Policy>>().Setup(r => r.GetEnumerator()).Returns(returnQueryable.GetEnumerator());
            mockDbSet.As<IQueryable<Policy>>().Setup(r => r.Provider).Returns(returnQueryable.Provider);
            mockDbSet.As<IQueryable<Policy>>().Setup(r => r.ElementType).Returns(returnQueryable.ElementType);
            mockDbSet.As<IQueryable<Policy>>().Setup(r => r.Expression).Returns(returnQueryable.Expression);

            // Test
            var returnedObjects = uut.GetAll();

            // Verify
            Assert.IsTrue(returnQueryable.SequenceEqual(returnedObjects));
        }

        [TestMethod]
        public void TestThatAddPerformsDbContextAdd()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testEntity = new Policy();

            // Test
            uut.Add(testEntity);

            // Verify
            mockDbSet.Verify(dbSet => dbSet.Add(testEntity));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddHandlesNullArgument()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test and Verify
            uut.Add(null);
        }

        [TestMethod]
        public void TestThatRemovePerformsDbContextRemove()
        {
            // Setup
            var uut = CreateUnitUnderTest();
            var testEntity = new Policy();

            // Test
            uut.Remove(testEntity);

            // Verify
            mockDbSet.Verify(dbSet => dbSet.Remove(testEntity));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveHandlesNullArgument()
        {
            // Setup
            var uut = CreateUnitUnderTest();

            // Test and Verify
            uut.Remove(null);
        }
    }
}

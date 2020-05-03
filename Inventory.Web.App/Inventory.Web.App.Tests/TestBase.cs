using AutoFixture;
using Moq;
using NUnit.Framework;
using System.Diagnostics;

namespace Inventory.Web.App.Tests
{
    public class TestBase
    {
        /// <summary>
        /// Gets the mock repository.
        /// </summary>
        /// <value>
        /// The mock repository.
        /// </value>
        protected MockRepository _mockRepository { get; private set; }

        private Stopwatch _stopwatch;

        protected Fixture _fixture { get; private set; }

        [SetUp]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _stopwatch = Stopwatch.StartNew();
            _fixture = new Fixture();
        }

        [TearDown]
        public void TestCleanup()
        {
            _mockRepository.VerifyAll();
            _stopwatch.Stop();
            Trace.WriteLine("***************************************************");
            Trace.WriteLine(string.Format("* Elapsed time for test (milliseconds): {0}", _stopwatch.Elapsed.TotalMilliseconds));
            Trace.WriteLine("***************************************************");
        }
    }
}

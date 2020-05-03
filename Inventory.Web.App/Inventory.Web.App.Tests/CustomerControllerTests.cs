using AutoFixture;
using FluentAssertions;
using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Inventory.Web.App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Inventory.Web.App.Tests
{
    [TestFixture]
    public class CustomerControllerTests: TestBase
    {
        private Mock<IInventoryDataProvider> _mockInventoryDataProvider;
        private CustomerController _sut;

        [SetUp]
        public void SetUp()
        {
            _mockInventoryDataProvider = _mockRepository.Create<IInventoryDataProvider>();
            _sut = new CustomerController(_mockInventoryDataProvider.Object);
        }

        [Test]
        public void Constructor_With_Null_Data_Provider_Raises_Exception()
        {
            Action action = () => { var test = new CustomerController(null); };
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Get_Groceries_With_Error_While_Getting_Groceries_From_Inventory_Raises_Exception()
        {
            // Arrange
            _mockInventoryDataProvider.Setup(x => x.Retrieve()).ThrowsAsync(new SystemException());

            // Act
            Func<Task> action = async () => { await _sut.Groceries(); };

            // Assert
            action.Should().Throw<SystemException>();
        }

        [Test]
        public async Task Get_Groceries_With_Valid_Request_Returns_Groceries()
        {
            // Arrange
            var results = _fixture.CreateMany<Fruit>(2);
            _mockInventoryDataProvider.Setup(x => x.Retrieve()).ReturnsAsync(results);

            // Act
            var result = await _sut.Groceries();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}

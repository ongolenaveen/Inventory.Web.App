using AutoFixture;
using FluentAssertions;
using Inventory.Core.DomainModels;
using Inventory.Core.Interfaces;
using Inventory.Web.App.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Inventory.Web.App.Tests
{
    [TestFixture]
    public class AdminControllerTests:TestBase
    {
        private Mock<IInventoryDataProvider> _mockInventoryDataProvider;
        private AdminController _sut;

        [SetUp]
        public void SetUp()
        {
            _mockInventoryDataProvider = _mockRepository.Create<IInventoryDataProvider>();
            _sut = new AdminController(_mockInventoryDataProvider.Object);
        }

        [Test]
        public void Constructor_With_Null_Data_Provider_Raises_Exception()
        {
            Action action = () => { var test = new AdminController(null); };
            action.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Get_Upload_Returns_View_Response()
        {
            var result = _sut.Upload();
            result.Should().BeOfType<ViewResult>();
        }

        [Test]
        public async Task Post_Upload_With_Empty_File_Returns_Bad_Request()
        {
            var result = await _sut.Upload(null);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void Post_Upload_With_Error_While_Uploading_File_To_Inventory_Raises_Exception()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.SetupGet(x => x.FileName).Returns("Inventory.csv");
            using var memoryStream = new MemoryStream(new byte[255]);
            using var writer = new StreamWriter(memoryStream);
            writer.WriteLine("Hello world");
            writer.Flush();
            mockFile.Setup(x => x.OpenReadStream()).Returns(memoryStream);

            var files = new List<IFormFile>() { mockFile.Object };

            _mockInventoryDataProvider.Setup(x => x.Upload(It.IsAny<InventoryFile>())).ThrowsAsync(new SystemException());

            // Act
            Func<Task> action = async () => { await _sut.Upload(files); };

            // Assert
            action.Should().Throw<SystemException>();
        }

        [Test]
        public async Task Post_Upload_With_Valid_File_Returns_Redirection_Result()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.SetupGet(x => x.FileName).Returns("Inventory.csv");
            using var memoryStream = new MemoryStream(new byte[255]);
            using var writer = new StreamWriter(memoryStream);
            writer.WriteLine("Hello world");
            writer.Flush();
            mockFile.Setup(x => x.OpenReadStream()).Returns(memoryStream);

            var files = new List<IFormFile>() { mockFile.Object };

            _mockInventoryDataProvider.Setup(x => x.Upload(It.IsAny<InventoryFile>())).Returns(Task.FromResult<object>(null));

            // Act
            var result =  await _sut.Upload(files);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
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

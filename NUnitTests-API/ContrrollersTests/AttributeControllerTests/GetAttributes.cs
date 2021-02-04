using CollectionMarket_API.Contracts;
using CollectionMarket_API.Controllers;
using CollectionMarket_UI.Filters;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests_API.ContrrollersTests.AttributeControllerTests
{
    [TestFixture]
    public class GetAttributes
    {
        private Mock<IAttributeService> _mockAttributeService;
        private Mock<ILoggerService> _mockLoggerService;
        private AttributesController _classUnderTest;
            

        [SetUp]
        public void Setup()
        {
            _mockAttributeService = new Mock<IAttributeService>();
            _mockLoggerService = new Mock<ILoggerService>();
            _classUnderTest = new AttributesController(_mockAttributeService.Object, _mockLoggerService.Object);
        }

        [Test]
        public async Task Should_CallServiceOnceAndReturnOK_When_FiltersObjectIsNotNull()
        {
            //arrange
            var filters = new AttributeFilters()
            {
                CategoryId = 1
            };

            //act
            var result = await _classUnderTest.GetAttributes(filters);

            //assert
            _mockAttributeService.Verify(v => v.GetFiltered(It.IsAny<AttributeFilters>()), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Should_CallServiceOnceAndReturnOK_When_FiltersObjectIsNull()
        {
            //act
            var result = await _classUnderTest.GetAttributes(null);

            //assert
            _mockAttributeService.Verify(v => v.GetFiltered(null), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}

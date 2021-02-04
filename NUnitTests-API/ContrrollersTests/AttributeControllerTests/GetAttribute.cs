using CollectionMarket_API.Contracts;
using CollectionMarket_API.Controllers;
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
    public class GetAttribute
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
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Should_DontCallServiceAndReturnBadRequestResult_When_IdIsLessThan1(int id)
        {
            //arrange
            _mockAttributeService
                .Setup(s => s.Exists(It.Is<int>(p => p == id)))
                .Returns(Task.FromResult(true));

            //act
            var result = await _classUnderTest.GetAttribute(id);

            //assert
            _mockAttributeService.Verify(v => v.Get(It.Is<int>(x => x == id)), Times.Never);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Should_LogErrorAndReturnStatusCode_When_ThrowException()
        {
            //arrange
            int id = 1;
            _mockAttributeService
                .Setup(s => s.Exists(It.Is<int>(p => p == id)))
                .Returns(Task.FromResult(true));
            _mockAttributeService
                .Setup(s => s.Get(It.IsAny<int>()))
                .Throws(new Exception());

            //act
            var result = await _classUnderTest.GetAttribute(id);

            //assert
            _mockAttributeService.Verify(v => v.Exists(It.Is<int>(x => x == id)), Times.Once);
            _mockAttributeService.Verify(v => v.Get(It.Is<int>(x => x == id)), Times.Once);
            _mockLoggerService.Verify(v => v.LogException(It.IsAny<Exception>()), Times.Once);
            Assert.IsInstanceOf<StatusCodeResult>(result);
        }

        [Test]
        public async Task Should_ReturnNotFound_When_AttributeNotExists()
        {
            //arrange
            int id = 1;
            _mockAttributeService
                .Setup(s => s.Exists(It.Is<int>(p => p == id)))
                .Returns(Task.FromResult(false));

            //act
            var result = await _classUnderTest.GetAttribute(id);

            //assert
            _mockAttributeService.Verify(v => v.Exists(It.Is<int>(x => x == id)), Times.Once);
            _mockAttributeService.Verify(v => v.Get(It.Is<int>(x => x == id)), Times.Never);
            _mockLoggerService.Verify(v => v.LogException(It.IsAny<Exception>()), Times.Never);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

    }
}

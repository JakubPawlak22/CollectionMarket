using AutoMapper;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests_API.ServicesTests.AttributeServiceTests
{
    [TestFixture]
    public class Exists
    {
        private Mock<IAttributeRepository> _mockAttributeRepo;
        private Mock<IMapper> _mockMapper;
        private AttributeService _classUnderTest;


        [SetUp]
        public void Setup()
        {
            _mockAttributeRepo = new Mock<IAttributeRepository>();
            _mockMapper = new Mock<IMapper>();
            _classUnderTest = new AttributeService(_mockAttributeRepo.Object, _mockMapper.Object);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnExistsMethodResponse(bool repositoryResponse)
        {
            //arrange
            int id = 1;
            _mockAttributeRepo
                .Setup(s => s.Exists(It.Is<int>(p => p == id)))
                .Returns(Task.FromResult(repositoryResponse));

            //act
            var result = await _classUnderTest.Exists(id);

            //assert
            _mockAttributeRepo.Verify(v => v.Exists(It.Is<int>(x => x == id)), Times.Once);
            Assert.AreEqual(repositoryResponse, result);
        }
    }
}

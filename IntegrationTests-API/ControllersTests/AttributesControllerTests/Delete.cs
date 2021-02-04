using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Controllers;
using CollectionMarket_API.Data;
using CollectionMarket_API.Mappings;
using CollectionMarket_API.Services;
using CollectionMarket_API.Services.Repositories;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests_API.ControllersTests.AttributesControllerTests
{
    [TestFixture]
    public class Delete
    {
        private IAttributeRepository _attributeRepo;
        private IMapper _mapper;
        private IAttributeService _attributeService;
        private ILoggerService _logger;
        private AttributesController _controller;
        private ApplicationDbContext _context;
        private int _attributeId;

        #region setup
        [SetUp]
        public void Setup()
        {
            CreateInMemoryDatabase();
            AddAttributeToDatabase();
            CreateRepository();
            CreateMapper();
            CreateLogger();
            CreateService();
            CreateController();
        }

        private void CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
        }
        private void AddAttributeToDatabase()
        {
            var attribute = new CollectionMarket_API.Data.Attribute()
            {
                DataType = (int)DataTypes.Text,
                Name = "Text"
            };
            _context.Add(attribute);
            _context.SaveChanges();
            _attributeId = attribute.Id;

        }


        private void CreateRepository()
        {
            _attributeRepo = new AttributeRepository(_context);
        }
        private void CreateMapper()
        {
            _mapper = null;
        }

        private void CreateLogger()
        {
            _logger = new LoggerService();
        }

        private void CreateService()
        {
            _attributeService = new AttributeService(_attributeRepo, _mapper);
        }

        private void CreateController()
        {
            _controller = new AttributesController(_attributeService, _logger);
        }
        #endregion setup

        [Test]
        public async Task Should_DeleteAttributeAndReturn204_When_EverythingIsOk()
        {
            var attributesCountBeforeTest = await _context.Attributes.CountAsync();

            //act
            var result = await _controller.Delete(_attributeId);

            //assert
            Assert.IsTrue(await _context.Attributes.CountAsync() == attributesCountBeforeTest-1);
            Assert.IsInstanceOf<StatusCodeResult>(result);
        }

        [Test]
        public async Task Should_ReturnBadRequest_When_IdIsLessThan1()
        {
            var attributesCountBeforeTest = await _context.Attributes.CountAsync();

            //act
            var result = await _controller.Delete(-1);

            //assert
            Assert.IsTrue(await _context.Attributes.CountAsync() == attributesCountBeforeTest);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Should_ReturnNotFound_When_AttributeDoesntExist()
        {
            var attributesCountBeforeTest = await _context.Attributes.CountAsync();

            //act
            var result = await _controller.Delete(_attributeId + 1);
            var asd = await _context.Attributes.CountAsync();
            //assert
            Assert.IsTrue(await _context.Attributes.CountAsync() == attributesCountBeforeTest);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}

﻿using Book.AutoMapper;
using Book.Data;
using Book.Data.Entities;
using Book.Dto;
using Book.LoggerProvider;
using Book.Services;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Book.UnitTests
{
    [TestClass]
    public class PublisherTest
    {
        private DataContext dataContext;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dataContext = new DataContext(options);

            AutoMapperConfiguration.Configure();
        }

        [TestCleanup]
        public void CleanUp()
        {
            dataContext.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetById_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new PublisherDto { PublisherName = "test" }.PublisherName);

            var service = new PublisherService(dataContext, mock.Object);
            await service.GetById(22);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Update_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new PublisherDto { PublisherName = "test" }.PublisherName);

            var service = new PublisherService(dataContext, mock.Object);
            await service.Update(888, new PublisherDto());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Delete_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new PublisherDto { PublisherName = "test" }.PublisherName);

            var service = new PublisherService(dataContext, mock.Object);
            await service.Delete(555);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordAlreadyExistException))]
        public async Task Create_RecordAlreadyExist()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new PublisherDto { PublisherName = "test" }.PublisherName);

            dataContext.Publishers.Add(new Publisher { PublisherId = 1, PublisherName = "LightHouse Publishing" });
            await dataContext.SaveChangesAsync();

            var publisherDto = new PublisherDto
            {
                PublisherName = "LightHouse Publishing"
            };

            var service = new PublisherService(dataContext, mock.Object);

            await service.Create(publisherDto);

        }
    }
}

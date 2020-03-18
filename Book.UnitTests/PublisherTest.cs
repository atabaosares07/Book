using Book.Data;
using Book.Dto;
using Book.Services;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var service = new PublisherService(dataContext);
            await service.GetById(22);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Update_NotFound()
        {
            var service = new PublisherService(dataContext);
            await service.Update(888, new PublisherDto());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Delete_NotFound()
        {
            var service = new PublisherService(dataContext);
            await service.Delete(555);
        }
    }
}

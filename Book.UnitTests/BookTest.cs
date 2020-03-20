using Book.Data;
using Book.Dto;
using Book.Logger;
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
    public class BookTest
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
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new BookDto { BookName = "The Little Prince" }.BookName);

            var service = new BookService(dataContext, mock.Object);
            await service.GetById(81);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Update_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new BookDto { BookName = "The Little Prince" }.BookName);

            var service = new BookService(dataContext, mock.Object);
            await service.Update(3344, new BookDto());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Delete_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new BookDto { BookName = "The Little Prince" }.BookName);

            var service = new BookService(dataContext, mock.Object);
            await service.Delete(777);
        }
    }
}

using Book.AutoMapper;
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
    public class CategoryTest
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
            mock.Setup(o => o.Log(param)).Returns(new CategoryDto { CategoryName = "test"}.CategoryName);

            var service = new CategoryService(dataContext, mock.Object);
            await service.GetById(999999);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Update_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new CategoryDto { CategoryName = "test" }.CategoryName);

            var service = new CategoryService(dataContext, mock.Object);
            await service.Update(787, new CategoryDto());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Delete_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new CategoryDto { CategoryName = "test" }.CategoryName);

            var service = new CategoryService(dataContext, mock.Object);
            await service.Delete(888);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordAlreadyExistException))]
        public async Task Create_RecordAlreadyExist()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new CategoryDto { CategoryName = "test" }.CategoryName);

            dataContext.Categories.Add(new Category { CategoryId = 1, CategoryName = "Philosophy" });
            dataContext.SaveChanges();

            var categoryDto = new CategoryDto
            {
                CategoryName = "Philosophy"
            };

            var service = new CategoryService(dataContext, mock.Object);

            await service.Create(categoryDto);
        }
    }
}

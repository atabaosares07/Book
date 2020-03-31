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
    public class AuthorTest
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
            mock.Setup(o => o.Log(param)).Returns(new AuthorDto { FirstName = "Stephen", LastName = "King" }.FirstName);

            var service = new AuthorService(dataContext, mock.Object);
            await service.GetById(812);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Update_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new AuthorDto { FirstName = "Stephen", LastName = "King" }.FirstName);

            var service = new AuthorService(dataContext, mock.Object);
            await service.Update(333, new AuthorDto());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Delete_NotFound()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new AuthorDto { FirstName = "Stephen", LastName = "King" }.FirstName);

            var service = new AuthorService(dataContext, mock.Object);
            await service.Delete(214);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordAlreadyExistException))]
        public async Task Create_RecordAlreadyExist()
        {
            var param = DateTime.Now.ToString();
            var mock = new Mock<ILogger>();
            mock.Setup(o => o.Log(param)).Returns(new AuthorDto { FirstName = "Stephen", LastName = "King" }.FirstName);

            dataContext.Authors.Add(new Author
            {
                AuthorId = 1,
                FirstName = "Stephen",
                LastName = "King"
            });

            await dataContext.SaveChangesAsync();

            var authorDto = new AuthorDto
            {
                FirstName = "Stephen",
                LastName = "King"
            };

            var service = new AuthorService(dataContext, mock.Object);

            await service.Create(authorDto);
        }
    }
}

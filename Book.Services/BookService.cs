using AutoMapper;
using Book.Data;
using Book.Dto;
using Book.LoggerProvider;
using Book.Services.Base;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services
{
    public class BookService: IBookService
    {
        private DataContext context;
        private readonly ILogger logger;

        public BookService(DataContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<object> Create(BookDto dto)
        {
            var entity = Mapper.Map<Book.Data.Entities.Book>(dto);
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity.BookId;
        }

        public async Task Delete(int id)
        {
            var entity = await context.Books.SingleOrDefaultAsync(o => o.BookId == id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookQueryDto>> GetAll()
        {
            return Mapper.Map<IEnumerable<BookQueryDto>>(await context.Books.ToListAsync());
        }

        public async Task<BookQueryDto> GetById(int id)
        {
            var entity = await context.Books.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            return Mapper.Map<BookQueryDto>(entity);
        }

        public async Task Update(int id, BookDto dto)
        {
            var entity = await context.Books.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            context.Update(Mapper.Map(dto, entity));
            await context.SaveChangesAsync();
        }
    }
}

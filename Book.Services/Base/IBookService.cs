using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
    public interface IBookService
    {
        Task<BookQueryDto> GetById(int id);
        Task<IEnumerable<BookQueryDto>> GetAll();
        Task<object> Create(BookDto dto);
        Task Update(int id, BookDto dto);
        Task Delete(int id);
    }
}

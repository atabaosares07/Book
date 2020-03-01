using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
    public interface IAuthorService
    {
        Task<AuthorQueryDto> GetById(int id);
        Task<IEnumerable<AuthorQueryDto>> GetAll();
        Task<object> Create(AuthorDto dto);
        Task Update(int id, AuthorDto dto);
        Task Delete(int id);
    }
}

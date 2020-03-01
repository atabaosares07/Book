using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
    public interface ICategoryService
    {
        Task<CategoryQueryDto> GetById(int id);
        Task<IEnumerable<CategoryQueryDto>> GetAll();
        Task<object> Create(CategoryDto dto);
        Task Update(int id, CategoryDto dto);
        Task Delete(int id);
    }
}

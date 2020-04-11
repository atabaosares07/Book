using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAll();
        Task<RoleDto> GetById(int id);
        Task<object> Create(RoleDto dto);
        Task Update(int id, RoleDto dto);
        Task Delete(int id);
    }
}

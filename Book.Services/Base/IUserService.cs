using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
	public interface IUserService
	{
		Task<UserDto> Authenticate(string username, string password);
		Task<IEnumerable<UserDto>> GetAll();
		Task<UserDto> GetById(int id);
		Task<UserDto> Create(RegisterDto dto);
		Task Update(UserDto dto, string password = null);
		Task UpdateRoles(int id, List<RoleDto> roles);
		Task Delete(int id);
	}
}

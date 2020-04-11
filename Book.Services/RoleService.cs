using AutoMapper;
using Book.Data;
using Book.Data.Entities;
using Book.Dto;
using Book.Services.Base;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services
{
	public class RoleService : IRoleService
    {
		private DataContext context;

		public RoleService(DataContext context)
		{
			this.context = context;
		}

		public async Task<object> Create(RoleDto dto)
		{
			var entity = Mapper.Map<Role>(dto);
			context.Add(entity);
			await context.SaveChangesAsync();
			return entity.RoleId;
		}

		public async Task Delete(int id)
		{
			var entity = await context.Roles.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}
			context.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<RoleDto>> GetAll()
		{
			return Mapper.Map<IEnumerable<RoleDto>>(await context.Roles.ToListAsync());
		}

		public async Task<RoleDto> GetById(int id)
		{
			var entity = await context.Roles.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}
			return Mapper.Map<RoleDto>(entity);
		}

		public async Task Update(int id, RoleDto dto)
		{
			var entity = await context.Roles.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}

			entity.RoleName = dto.RoleName;

			context.Update(entity);
			await context.SaveChangesAsync();
		}
	}
}

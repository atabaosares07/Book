using AutoMapper;
using Book.Data;
using Book.Data.Entities;
using Book.Dto;
using Book.LoggerProvider;
using Book.Services.Base;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services
{
	public class AuthorService : IAuthorService
    {
		private DataContext context;
		private readonly ILogger logger;

		public AuthorService(DataContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public async Task<object> Create(AuthorDto dto)
		{
			var entity = Mapper.Map<Author>(dto);
			context.Add(entity);
			await context.SaveChangesAsync();
			return entity.AuthorId;
		}

		public async Task Delete(int id)
		{
			var entity = await context.Authors.SingleOrDefaultAsync(o => o.AuthorId == id);
			if (entity == null)
			{
				throw new NotFoundException();
			}

			context.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AuthorQueryDto>> GetAll()
		{
			return Mapper.Map<IEnumerable<AuthorQueryDto>>(await context.Authors.ToListAsync());
		}

		public async Task<AuthorQueryDto> GetById(int id)
		{
			var entity = await context.Authors.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}
			return Mapper.Map<AuthorQueryDto>(entity);
		}

		public async Task Update(int id, AuthorDto dto)
		{
			var entity = await context.Authors.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}

			context.Update(Mapper.Map(dto, entity));
			await context.SaveChangesAsync();
		}
	}
}

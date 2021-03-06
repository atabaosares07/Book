﻿using AutoMapper;
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
	public class CategoryService : ICategoryService
    {
		private DataContext context;
		private readonly ILogger logger;

		public CategoryService(DataContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public async Task<object> Create(CategoryDto dto)
		{
			if (await context.Categories.AnyAsync(c => c.CategoryName == dto.CategoryName))
				throw new RecordAlreadyExistException();

			var entity = Mapper.Map<Category>(dto);

			context.Add(entity);
			await context.SaveChangesAsync();
			return entity.CategoryId;
		}

		public async Task Delete(int id)
		{
			var entity = await context.Categories.SingleOrDefaultAsync(o => o.CategoryId == id);
			if (entity == null)
			{
				throw new NotFoundException();
			}

			context.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CategoryQueryDto>> GetAll()
		{
			return Mapper.Map<IEnumerable<CategoryQueryDto>>(await context.Categories.Include(c => c.Books).ToListAsync());
		}

		public async Task<CategoryQueryDto> GetById(int id)
		{
			var entity = await context.Categories.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}
			return Mapper.Map<CategoryQueryDto>(entity);
		}

		public async Task Update(int id, CategoryDto dto)
		{
			var entity = await context.Categories.FindAsync(id);
			if (entity == null)
			{
				throw new NotFoundException();
			}

			context.Update(Mapper.Map(dto, entity));
			await context.SaveChangesAsync();
		}
	}
}

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
    public class PublisherService : IPublisherService
    {
        private readonly DataContext context;
        private readonly ILogger logger;

        public PublisherService(DataContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<object> Create(PublisherDto dto)
        {
            var entity = Mapper.Map<Publisher>(dto);
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity.PublisherId;
        }

        public async Task Delete(int id)
        {
            var entity = await context.Publishers.SingleOrDefaultAsync(p => p.PublisherId == id);
            if (entity == null)
                throw new NotFoundException();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PublisherQueryDto>> GetAll()
        {
            return Mapper.Map<IEnumerable<PublisherQueryDto>>(await context.Publishers.Include(p => p.Books).ToListAsync());
        }

        public async Task<PublisherQueryDto> GetById(int id)
        {
            var entity = await context.Publishers.FindAsync(id);
            if (entity == null)
                throw new NotFoundException();
            return Mapper.Map<PublisherQueryDto>(entity);
        }

        public async Task Update(int id, PublisherDto dto)
        {
            var entity = await context.Publishers.FindAsync(id);
            if (entity == null)
                throw new NotFoundException();
            context.Update(Mapper.Map(dto, entity));
            await context.SaveChangesAsync();
        }
    }
}

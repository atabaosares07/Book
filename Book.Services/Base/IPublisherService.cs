using Book.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Services.Base
{
    public interface IPublisherService
    {
        Task<PublisherQueryDto> GetById(int id);
        Task<IEnumerable<PublisherQueryDto>> GetAll();
        Task<object> Create(PublisherDto dto);
        Task Update(int id, PublisherDto dto);
        Task Delete(int id);
    }
}

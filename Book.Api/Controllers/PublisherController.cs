using Book.Dto;
using Book.Services.Base;
using Book.Services.Rules.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService PublisherService;

        public PublisherController(IPublisherService PublisherService)
        {
            this.PublisherService = PublisherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherQueryDto>>> Get()
        {
            return Ok(await PublisherService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherQueryDto>> Get(int id)
        {
            try
            {
                return Ok(await PublisherService.GetById(id));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PublisherDto dto)
        {
            try
            {
                return Ok(await PublisherService.Create(dto));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PublisherDto dto)
        {
            try
            {
                await PublisherService.Update(id, dto);
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await PublisherService.Delete(id);
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }
    }
}

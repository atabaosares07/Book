using Book.Dto;
using Book.Services.Base;
using Book.Services.Rules.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService AuthorService;

        public AuthorController(IAuthorService AuthorService)
        {
            this.AuthorService = AuthorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorQueryDto>>> Get()
        {
            return Ok(await AuthorService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorQueryDto>> Get(int id)
        {
            try
            {
                return Ok(await AuthorService.GetById(id));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuthorDto dto)
        {
            try
            {
                return Ok(await AuthorService.Create(dto));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AuthorDto dto)
        {
            try
            {
                await AuthorService.Update(id, dto);
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
                await AuthorService.Delete(id);
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }
    }
}

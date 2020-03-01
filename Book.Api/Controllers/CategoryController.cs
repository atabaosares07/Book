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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryQueryDto>>> Get()
        {
            return Ok(await categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryQueryDto>> Get(int id)
        {
            try
            {
                return Ok(await categoryService.GetById(id));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto dto)
        {
            try
            {
                return Ok(await categoryService.Create(dto));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDto dto)
        {
            try
            {
                await categoryService.Update(id, dto);
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
                await categoryService.Delete(id);
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }
    }
}

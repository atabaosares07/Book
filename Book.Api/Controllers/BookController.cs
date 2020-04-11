using Book.Dto;
using Book.Services.Base;
using Book.Services.Rules.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;

        public BookController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookQueryDto>>> Get()
        {
            return Ok(await BookService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookQueryDto>> Get(int id)
        {
            try
            {
                return Ok(await BookService.GetById(id));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookDto dto)
        {
            try
            {
                return Ok(await BookService.Create(dto));
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BookDto dto)
        {
            try
            {
                await BookService.Update(id, dto);
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
                await BookService.Delete(id);
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }
    }
}

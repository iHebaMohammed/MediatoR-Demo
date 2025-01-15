using Demo.Application.Feature.Preson.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var query = new GetAllPresonQurey();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult> Get(int id) 
        {
            var query = new GetPresonByIdQurey(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return NotFound(result);
        }
    }
}

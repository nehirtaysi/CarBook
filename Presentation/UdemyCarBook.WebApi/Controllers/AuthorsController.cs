using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.AuthorCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.AuthorQueires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetAuthorQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetAuthorByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yazar başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveAuthorCommand(id));
            return Ok("Yazar başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yazar başarıyla güncellendi.");
        }
    }
}

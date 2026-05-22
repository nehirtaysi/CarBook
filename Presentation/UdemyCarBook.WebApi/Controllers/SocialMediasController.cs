using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.SocialMediaCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.SocialMedia_Queires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SocialMediasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetSocialMediaQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetSocialMediaByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreateSocialMediaCommand command)
        {
            await _mediator.Send(command);
            return Ok("Sosyal Medya başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveSocialMediaCommand(id));
            return Ok("Sosyal Medya başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdateSocialMediaCommand command)
        {
            await _mediator.Send(command);
            return Ok("Sosyal Medya başarıyla güncellendi.");
        }
    }
}

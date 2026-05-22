using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.ServiceCCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.ServiceQueires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetServiceQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetServiceByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreateServiceCommand command)
        {
            await _mediator.Send(command);
            return Ok("Hizmet başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveServiceCommand(id));
            return Ok("Hizmet başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdateServiceCommand command)
        {
            await _mediator.Send(command);
            return Ok("Hizmet başarıyla güncellendi.");
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UdemyCarBook.Application.Features.Mediatör.Commands.PricingCommands;

using UdemyCarBook.Application.Features.Mediatör.Queires.Pricing_Quieres;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PricingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetPricingQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetPricingByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreatePricingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ödeme Türü başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemovePricingCommand(id));
            return Ok("Ödeme Türü başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdatePricingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ödeme Türü başarıyla güncellendi.");
        }
    }
}

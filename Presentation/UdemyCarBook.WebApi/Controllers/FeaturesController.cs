using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.FeatureCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.FeatureQueires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult>FeatureList()
        {
            var value = await _mediator.Send(new GetFeatureQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]
        
        public async Task<IActionResult>GetFeature(int id)
        {
            var value = await _mediator.Send(new GetFeatureByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateFeature(CreateFeatureCommand command)
        {
            await _mediator.Send(command);
            return Ok("Özellik başarıyla eklnedl.");
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveFeatureCommand(id));
            return Ok("Özellik başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateFeature(UpdateFeatureCommand command)
        {
            await _mediator.Send(command);
            return Ok("Özellik başarıyla güncellendi.");
        }
    }
}

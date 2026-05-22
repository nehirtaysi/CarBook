using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.TestimonialCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.TestimonialQueires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestimonialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetTestimonialQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetTestimonialByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreateTestimonialCommand command)
        {
            await _mediator.Send(command);
            return Ok("Referans başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveTestimonialCommand(id));
            return Ok("Referans başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdateTestimonialCommand command)
        {
            await _mediator.Send(command);
            return Ok("Referans başarıyla güncellendi.");
        }
    }
}

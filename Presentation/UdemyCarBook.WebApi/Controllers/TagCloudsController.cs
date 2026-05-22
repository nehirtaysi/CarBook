using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediatör.Commands.TagCloudCommands;
using UdemyCarBook.Application.Features.Mediatör.Queires.TagCloudQueires;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagCloudsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> LocaitonList()
        {
            var value = await _mediator.Send(new GetTagCloudQuery());
            return Ok(value);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetLocaiton(int id)
        {
            var value = await _mediator.Send(new GetTagCloudByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]

        public async Task<IActionResult> CreateLocaiton(CreateTagCloudCommand command)
        {
            await _mediator.Send(command);
            return Ok("Etiket Bulutu başarıyla eklnedl.");
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveFeauture(int id)
        {
            await _mediator.Send(new RemoveTagCloudCommand(id));
            return Ok("Etiket Bulutu başasıyla silindi.");
        }
        [HttpPut]

        public async Task<IActionResult> UpdateLocaiton(UpdateTagCloudCommand command)
        {
            await _mediator.Send(command);
            return Ok("Etiket Bulutu başarıyla güncellendi.");
        }

        [HttpGet("GetTagCloudByBlogId")]
        public async Task<IActionResult> GetTagCloudByBlogId(int id)
        {
            var values = await _mediator.Send(new GetTagCloudByBlogIdQuery(id));
            return Ok(values);
        }


    }
}

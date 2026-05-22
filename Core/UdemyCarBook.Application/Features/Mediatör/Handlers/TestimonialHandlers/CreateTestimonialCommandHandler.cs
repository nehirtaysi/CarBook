using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Commands.TestimonialCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.TestimonialHandlers
{
   
   
        public class CreateTestimonialCommandHandler : IRequestHandler<CreateTestimonialCommand>
        {

            private readonly IRepository<Testimonial> _repository;

            public CreateTestimonialCommandHandler
                (IRepository<Testimonial> repository)
            {
                _repository = repository;
            }

            public async Task Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
            {
                await _repository.CreateAsync(new Testimonial

                {
                   Comment = request.Comment,
                   ImageUrl = request.ImageUrl,
                   Name = request.Name,
                   Title = request.Title,


                });
            }
        }
    }


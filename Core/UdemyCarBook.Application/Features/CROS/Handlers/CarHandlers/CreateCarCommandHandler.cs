using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CROS.Commands.BrandCommand;
using UdemyCarBook.Application.Features.CROS.Commands.CarCommand;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CROS.Handlers.CarHandlers
{
    public class CreateCarCommandHandler
    {
        private readonly IRepository<Car> _repository;

        public CreateCarCommandHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task Handle( CreateCarCommand command)

        {
            await _repository.CreateAsync(new Car
            {
                Fuel = command.Fuel,
                CoverImageUrl = command.CoverImageUrl,
                BigImageUrl = command.BigImageUrl,
                Km = command.KM,
                Luggage = command.Luggage,
                Model = command.Model,
                BrandID = command.BrandID,
                Seat = command.Seat,
                Transmission = command.Transmission
              
            });
        }
    }
}

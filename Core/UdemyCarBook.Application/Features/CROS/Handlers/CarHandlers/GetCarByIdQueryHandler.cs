using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CROS.Handlers.BannerHandler;
using UdemyCarBook.Application.Features.CROS.Queries.BrandQueires;
using UdemyCarBook.Application.Features.CROS.Queries.CarQueires;
using UdemyCarBook.Application.Features.CROS.Results.BrandResults;
using UdemyCarBook.Application.Features.CROS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CROS.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly IRepository<Car> _repository;

        public GetCarByIdQueryHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }
        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetCarByIdQueryResult
            {
                BrandID = values.BrandID,
                CarID = values.CarID,
                Transmission = values.Transmission,
                Seat = values.Seat,
                Model   = values.Model,
                Luggage = values.Luggage,   
                KM = values.Km,
                BigImageUrl = values.BigImageUrl,
                CoverImageUrl = values.CoverImageUrl,
                Fuel = values.Fuel
            };
        }

        
    }
}

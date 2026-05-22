using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Queires.RentACarQueries;
using UdemyCarBook.Application.Features.Mediatör.Results.RentACarResults;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.RentACarHandlers
{
    public class GetRentACarQueryHandler : IRequestHandler<GetRentACarQuery, List<GetRentACarQueryResult>>
    {
        private readonly IRentACarRepository _repository;
        public GetRentACarQueryHandler(IRentACarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetRentACarQueryResult>> Handle(GetRentACarQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAvailableRentACarsByFilterAsync(request.LocationID, request.PickUpDate, request.DropOffDate);

            var results = values.Select(y => new GetRentACarQueryResult
            {
                CarId = y.CarID,
                Brand = y.Car?.Brand?.Name ?? "Marka Belirtilmemiş",
                Model = y.Car?.Model ?? "Model Belirtilmemiş",

                CoverImageUrl = y.Car?.CoverImageUrl ?? "",

                Transmission = y.Car?.Transmission ?? "Bilinmiyor",
                Fuel = y.Car?.Fuel ?? "Bilinmiyor",
                Amount = y.Car?.CarPricings?.FirstOrDefault(z => z.PricingID == 2)?.Amount ?? 0
            }).ToList();

            return results;
        }
    }
}
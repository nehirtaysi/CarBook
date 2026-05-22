using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Queires.CarPricingQueires;
using UdemyCarBook.Application.Features.Mediatör.Results.CarPricingResults;
using UdemyCarBook.Application.Features.Mediatör.Results.LocationResults;
using UdemyCarBook.Application.Interfaces.CarPricingInterfaces;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.CarPricingHandlers
{
    public class GetCarPricingWithCarQueryHandler : IRequestHandler<GetCarPricingWithCarQuery, 
        List<GetCarPricingWithCarQueryResult>>
    {
        private readonly ICarPricingRepository _repository;

        public GetCarPricingWithCarQueryHandler(ICarPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCarPricingWithCarQueryResult>> Handle(GetCarPricingWithCarQuery request, CancellationToken cancellationToken)
        {
            var values =  _repository.GetCarPricingWithCars();
            return values.Select(x => new GetCarPricingWithCarQueryResult
            {
               Amount   = x.Amount,
               Brand = x.Car.Brand.Name,
               CarPricingId=x.CarPricingID,
               CoverImageUrl = x.Car.CoverImageUrl,
               Model=x.Car.Model,
               CarId=x.CarID,
               Transmission = x.Car.Transmission,
               Fuel = x.Car.Fuel,
               CampaignExpiryDate=x.CampaignExpiryDate,
               IsCampaign = x.IsCampaign,
               CampaignPrice = x.CampaignPrice,
              
          
        }).ToList();
        }
    }
}

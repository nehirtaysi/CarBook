using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.CarPricingResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.CarPricingQueires
{
    public class GetCarPricingWithCarQuery:IRequest<List<GetCarPricingWithCarQueryResult>>
    {
    }
}

using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CROS.Results.BrandResults;
using UdemyCarBook.Application.Features.Mediatör.Queires.FeatureQueires;
using UdemyCarBook.Application.Features.Mediatör.Results.FeatureResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.FeatureHandler
{
    public class GetFeatureByIdQueryHandler : IRequestHandler<GetFeatureByIdQuery, GetFeatureByIdQueryResult>
    {
        private readonly IRepository<Feature> _repository;

        public GetFeatureByIdQueryHandler(IRepository<Feature> repository)
        {
            _repository = repository;
        }

        public async Task<GetFeatureByIdQueryResult> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
        {

            var values = await _repository.GetByIdAsync(request.Id);
            return new GetFeatureByIdQueryResult
            { 
               
                Name = values.Name,
                FeatureID = values.FeatureID,  
                
            };
        }
    }
}

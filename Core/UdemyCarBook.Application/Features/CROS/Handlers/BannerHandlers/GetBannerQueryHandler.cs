using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CROS.Results.BannerResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CROS.Handlers.BannerHandler
{
    public class GetBannerQueryHandler
    {
        private readonly IRepository<Banner> _repository;

        public GetBannerQueryHandler(IRepository<Banner> repository)
        {
            _repository = repository;
        }

        public async Task< List<GetBannerQueryResult>>Handle()
        {
            var values =await _repository.GetAllAsync();
            return values.Select(x=> new GetBannerQueryResult
                {
                BannerID = x.BannerID,
                Description = x.Description,
                Title= x.Title,
                VideoDescription= x.VideoDescription,
                VideoUrl= x.VideoUrl,

                }).ToList();
        }
    }
}

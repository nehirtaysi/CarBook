using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CROS.Results.AboutResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CROS.Handlers.AboutHandlers
{
    public class GetAboutQueryHandler
    {
        private readonly IRepository <About> _repository;

        public GetAboutQueryHandler(IRepository<About> repository)
        {
            _repository = repository;
        }
        public async Task< List<GetAboutByIdQueryResults>> Handle()
        {
            var values = await  _repository.GetAllAsync();
            return values.Select(x=> new GetAboutByIdQueryResults
            { AboutID=x.AboutID,
            Description=x.Description,
            Title=x.Title,
            ImageUrl=x.ImageUrl
            }).ToList();
        }

    }
}

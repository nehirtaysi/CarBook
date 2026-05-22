using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Queires.TagCloudQueires;
using UdemyCarBook.Application.Features.Mediatör.Results.TagCloudResults;
using UdemyCarBook.Application.Interfaces.TagCloudInterfaces;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.TagCloudHandlers
{
    public class GetTagCloudByBlogQueryHandler : IRequestHandler<GetTagCloudByBlogIdQuery, List<GetTagCloudByBlogIdQueryResult>>
    {
        private readonly ITagCloudRepository _repository;

        public GetTagCloudByBlogQueryHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetTagCloudByBlogIdQueryResult>> Handle(GetTagCloudByBlogIdQuery request, CancellationToken cancellationToken)
        {
            var values =  _repository.GetTagCloudsByBlogID(request.Id);
            return values.Select(x => new GetTagCloudByBlogIdQueryResult
            {

                TagCloudID = x.TagCloudID,
                BlogID = x.BlogID,

                Title = x.Title
            }).ToList();
        }
    }
}

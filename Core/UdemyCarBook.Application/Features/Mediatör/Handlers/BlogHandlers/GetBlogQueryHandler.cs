using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Queires.BlogQueires;
using UdemyCarBook.Application.Features.Mediatör.Results.BlogResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediatör.Handlers.BlogHandlers
{
    
   
        public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, List<GetBlogQueryResult>>
        {
            private readonly IRepository<Blog> _repository;

            public GetBlogQueryHandler(IRepository<Blog> repository)
            {
                _repository = repository;
            }

            public async Task<List<GetBlogQueryResult>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
            {
                var values = await _repository.GetAllAsync();
                return values.Select(x => new GetBlogQueryResult
                {
                    BlogID = x.BlogID,
                    Title = x.Title,
                    CoverImageUrl = x.CoverImageUrl,
                    CreatedDate = x.CreatedDate,
                    CategoryID = x.CategoryID,
                  
                    AuthorID = x.AuthorID   

                    
                }).ToList();
            }
        }
    }


using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Queires.AuthorQueires;
using UdemyCarBook.Application.Features.Mediatör.Results.AuthorResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Authors.Mediatör.Handlers.AuthorHandlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, GetAuthorByIdQueryResult>
    {
        private readonly IRepository<Author> _repository;

        public GetAuthorByIdQueryHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<GetAuthorByIdQueryResult> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {

            var values = await _repository.GetByIdAsync(request.Id);
            return new GetAuthorByIdQueryResult
            {

                Name = values.Name,
                AuthorID = values.AuthorID,
                ImageUrl = values.ImageUrl,
                Description=values.Description

            };
        }
    }
}

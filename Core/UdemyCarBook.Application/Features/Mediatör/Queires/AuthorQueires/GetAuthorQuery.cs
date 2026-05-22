using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.AuthorResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.AuthorQueires
{
    public class GetAuthorQuery:IRequest<List<GetAuthorQueryResult>>
    {
    }
}

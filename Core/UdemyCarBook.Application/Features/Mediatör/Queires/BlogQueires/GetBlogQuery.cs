using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediatör.Results.BlogResults;

namespace UdemyCarBook.Application.Features.Mediatör.Queires.BlogQueires
{
    public class GetBlogQuery:IRequest<List<GetBlogQueryResult>>
    {
    }
}

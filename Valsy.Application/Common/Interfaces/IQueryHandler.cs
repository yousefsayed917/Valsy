using MediatR;
using Valsy.Application.Common.Abstracts;

namespace Valsy.Application.Common.Interfaces
{
    public interface IQueryHandler<in TQuery, TResult> :
         IRequestHandler<TQuery, TResult>
         where TQuery : QueryBase<TResult>
    {
    }
}

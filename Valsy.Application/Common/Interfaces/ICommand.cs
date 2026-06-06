using MediatR;

namespace Valsy.Application.Common.Interfaces
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }
    public interface ICommand : IRequest
    {
        Guid Id { get; }
    }
}
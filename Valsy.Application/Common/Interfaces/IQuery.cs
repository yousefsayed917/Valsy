using BuildingBlocks.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Text.Json.Serialization;
using Valsy.Application.Common.Exceptions;

namespace Valsy.Application.Common.Interfaces
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
    public abstract class RequestBase : IRequest
    {
        [JsonIgnore]
        public readonly Guid Id;
        public RequestBase()
        {
            Id = Guid.NewGuid();
        }
        public void NotFound(string key, IStringLocalizer stringLocalizer)
        {
            throw new NotFoundException(stringLocalizer[key].Value, new List<(string, string)>());
        }
    }
    public abstract class RequestBase<TResult> : IRequest<TResult>
    {
        [JsonIgnore]
        public readonly Guid Id;
        public RequestBase()
        {
            Id = Guid.NewGuid();
        }
        public void NotFound(string key, IStringLocalizer stringLocalizer)
        {
            throw new NotFoundException(stringLocalizer[key].Value, new List<(string, string)>());
        }
    }

}
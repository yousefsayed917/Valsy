using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Enums;

namespace Valsy.Application.Common.Abstracts
{
    public abstract class CommandBase : RequestBase
    {
    }

    public abstract class CommandBase<TResult> : RequestBase<TResult>
    {
        public InitSource InitSource { get; set; } = InitSource.Api;
    }
}

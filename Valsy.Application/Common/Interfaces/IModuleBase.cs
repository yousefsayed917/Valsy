using Valsy.Application.Common.Abstracts;

namespace Valsy.Application.Common.Interfaces
{
    public interface IModuleBase
    {
        Task<TResult> ExecuteCommandAsync<TResult>(CommandBase<TResult> command);

        Task ExecuteCommandAsync(CommandBase command);

        Task<TResult> ExecuteQueryAsync<TResult>(QueryBase<TResult> query);
    }
}

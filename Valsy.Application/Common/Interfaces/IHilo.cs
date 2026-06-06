using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Common.RegisteringServices;

namespace Valsy.Application.Common.Interfaces
{
    public interface IHilo : IScopedService
    {
        Task<int> GenerateIntId(Type type, DbContext context);
    }
}

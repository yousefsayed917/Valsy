using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Common.RegisteringServices;

namespace Valsy.Application.Common.Interfaces
{
    public interface IHiloApplyer : IScopedService
    {
        Task ApplyEntity(Type type, DbContext context, Dictionary<Type, long> configurations);
        Task CreateNHiLoEntityIfNotExists();
    }
}

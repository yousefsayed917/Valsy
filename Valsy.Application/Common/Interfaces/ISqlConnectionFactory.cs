using System.Data;
using Valsy.Domain.Common.RegisteringServices;
namespace Valsy.Application.Common.Interfaces
{
    public interface ISqlConnectionFactory : IScopedService
    {
        IDbConnection GetOpenConnection();

        IDbConnection CreateNewConnection();

        string GetConnectionString();
    }
}

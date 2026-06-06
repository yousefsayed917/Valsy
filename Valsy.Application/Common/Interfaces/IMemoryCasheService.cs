using Valsy.Domain.Common.RegisteringServices;

namespace Valsy.Application.Common.Interfaces
{
    public interface IMemoryCasheService : ITransientService
    {
        T GetFromCache<T>(string key);
        T AddToCache<T>(string key, T value, int absoluteExpirationInSeconds = default, int slidingExpirationInSeconds = default);
        void RemoveFromCache(string key);
        void ClearCache();
    }
}

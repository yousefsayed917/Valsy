using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Infrastructure.Common.Repositories
{
    public class HiloGenerator : IHilo
    {
        public async Task<int> GenerateIntId(Type type, DbContext context)
        {
            // Simple implementation: returns sequential IDs
            // In production, you might use a proper Hi/Lo algorithm
            await Task.Delay(0); // Placeholder async call
            return new Random().Next(1, int.MaxValue);
        }
    }
}

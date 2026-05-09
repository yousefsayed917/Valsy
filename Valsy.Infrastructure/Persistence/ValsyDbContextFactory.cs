using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Valsy.Infrastructure;

public class ValsyDbContextFactory : IDesignTimeDbContextFactory<ValsyDbContext>
{
    public ValsyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ValsyDbContext>();
        var connectionString = "Server=127.0.0.1;Port=3306;Database=ValsyDb;User=valsy_app;Password=Valsy@123;";

        optionsBuilder.UseMySQL(connectionString);

        return new ValsyDbContext(optionsBuilder.Options);
    }
}

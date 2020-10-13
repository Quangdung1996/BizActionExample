using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BizActionExample.Infa.Migrations
{
    internal class Program
    {
        private static IServiceProvider BuildServiceProvider()
        {
            var assembly = typeof(TheApp).Assembly;

            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            var isc = new ServiceCollection();

            var connectionString = config.GetConnectionString("EfCore");
            isc.AddDbContext<EfCoreContext>(options => options.UseSqlServer(connectionString));
            return isc.BuildServiceProvider();
        }

        private static void Main(string[] args)
        {
            BuildServiceProvider().GetRequiredService<EfCoreContext>();
        }

        private class EfCoreContextFactory : IDesignTimeDbContextFactory<EfCoreContext>
        {
            public EfCoreContext CreateDbContext(string[] args)
            {
                return BuildServiceProvider().GetRequiredService<EfCoreContext>();
            }
        }
    }

    internal class TheApp
    {
        private readonly EfCoreContext _theContext;

        public TheApp(EfCoreContext theContext)
        {
            _theContext = theContext;
        }

        public void Run()
        {
            // Do something on _theContext
        }
    }
}
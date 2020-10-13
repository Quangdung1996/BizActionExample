using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizActionExample.Infa
{
    public static class InfraSetup
    {
        public static void AddEfCoreContext(this IServiceCollection services)
        {
            services.AddDbContext<EfCoreContext>((s, o) => o.UseSqlServer(s.GetRequiredService<IConfiguration>()[ConfigurationKeys.ConnectionStringName]));
        }
    }
}
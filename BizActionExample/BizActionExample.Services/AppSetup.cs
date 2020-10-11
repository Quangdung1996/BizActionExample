using GenericBizRunner.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BizActionExample.Services
{
    public static class AppSetup
    {
        public  static IServiceCollection RegisterServies(this IServiceCollection  services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            connection.Open();  //see https://github.com/aspnet/EntityFramework/issues/6968
            services.AddDbContext<EfCoreContext>(options => options.UseSqlite(connection));
            //--------------------------------------------------------------------

            #region GenericBizRunner parts
            //This sets up the GenericBizRunner to use one DbContext. Note: you could add a GenericBizRunnerConfig here if you needed it
            services.RegisterBizRunnerWithDtoScans<EfCoreContext>(Assembly.GetAssembly(typeof(AppSetup)));
            //This sets up the GenericBizRunner to work with multiple DbContext
            //see https://github.com/JonPSmith/EfCore.GenericBizRunner/wiki/Using-multiple-DbContexts
            //services.RegisterBizRunnerMultiDbContextWithDtoScans(Assembly.GetAssembly(typeof(WebChangeDeliveryDto)));
            #endregion

            //now we register the public classes with public interfaces in the three layers
            services.RegisterAssemblyPublicNonGenericClasses(
                Assembly.GetAssembly(typeof(CreatePaymentModel)), //Service layer
                Assembly.GetAssembly(typeof(CreatePaymentView)), //Service layer
                Assembly.GetAssembly(typeof(ProductAction))
            ).AsPublicImplementedInterfaces();

            return services;
        }
    }
}

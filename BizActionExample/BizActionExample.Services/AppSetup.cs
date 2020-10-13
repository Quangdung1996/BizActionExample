using AutoMapper;
using BizActionExample.Infa;
using BizActionExample.Services.BizActions;
using BizActionExample.Services.BizActions.Identity;
using BizActionExample.Services.Repositories;
using GenericBizRunner.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace BizActionExample.Services
{
    public static class AppSetup
    {
        public static IServiceCollection RegisterServies(this IServiceCollection services)
        {
            services.AddEfCoreContext();
            //--------------------------------------------------------------------

            #region GenericBizRunner parts

            //This sets up the GenericBizRunner to use one DbContext. Note: you could add a GenericBizRunnerConfig here if you needed it
            services.RegisterBizRunnerWithDtoScans<EfCoreContext>(Assembly.GetAssembly(typeof(AppSetup)));
            //This sets up the GenericBizRunner to work with multiple DbContext
            //see https://github.com/JonPSmith/EfCore.GenericBizRunner/wiki/Using-multiple-DbContexts
            //services.RegisterBizRunnerMultiDbContextWithDtoScans(Assembly.GetAssembly(typeof(WebChangeDeliveryDto)));

            #endregion GenericBizRunner parts

            //now we register the public classes with public interfaces in the three layers
            services.RegisterAssemblyPublicNonGenericClasses(
               Assembly.GetAssembly(typeof(RegisterUserAction))
            ).AsPublicImplementedInterfaces();

            services.AddScoped<IRegisterUserAction, RegisterUserAction>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(AppSetup).Assembly);
            return services;
        }
    }
}
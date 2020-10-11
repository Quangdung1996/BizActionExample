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

            //now we register the public classes with public interfaces in the three layers
            services.RegisterAssemblyPublicNonGenericClasses(
                Assembly.GetAssembly(typeof(CreatePaymentModel)), //Service layer
                Assembly.GetAssembly(typeof(CreatePaymentView))
            ).AsPublicImplementedInterfaces();

            return services;
        }
    }
}

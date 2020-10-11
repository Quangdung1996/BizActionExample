using Microsoft.EntityFrameworkCore;
using System;

namespace GenericBizRunner.Helpers
{
    public class ValidationDbContextServiceProvider : IServiceProvider
    {
        private readonly DbContext _currContext;

        public ValidationDbContextServiceProvider(DbContext currContext)
        {
            _currContext = currContext;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(DbContext))
            {
                return _currContext;
            }

            return null;
        }
    }
}
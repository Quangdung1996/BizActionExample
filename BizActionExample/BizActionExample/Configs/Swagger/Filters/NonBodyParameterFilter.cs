using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BizActionExample.Configs.Swagger.Filters
{
    public class NonBodyParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo);

            // Policy names map to scopes
            var controllerScopes = methodInfo
                .GetCustomAttributes()
                .OfType<NonBodyParameterAttribute>()
                .Select(attr => attr);

            var actionScopes = methodInfo
                .GetCustomAttributes()
                .OfType<NonBodyParameterAttribute>()
                .Select(attr => attr);

            var requiredScopes = controllerScopes.Union(actionScopes).Distinct();

            if (requiredScopes.Any())
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                foreach (var item in requiredScopes)
                {
                    operation.Parameters.Add(item.Parameter);
                }
            }
        }
    }
}

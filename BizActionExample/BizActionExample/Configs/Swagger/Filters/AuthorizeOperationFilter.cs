using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BizActionExample.Configs.Swagger.Filters
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo);

            // Policy names map to scopes
            var controllerScopes = methodInfo
                .GetCustomAttributes()
                .OfType<AuthorizeHeaderRequired>()
                .Select(attr => attr);

            var actionScopes = methodInfo
                .GetCustomAttributes()
                .OfType<AuthorizeHeaderRequired>()
                .Select(attr => attr);

            var allowAnonymous = methodInfo
                .GetCustomAttributes()
                .OfType<AllowAnonymousAttribute>()
                .Select(attr => attr).Any();

            var requiredScopes = controllerScopes.Union(actionScopes).Distinct();

            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            operation.Responses.Add("500", new OpenApiResponse { Description = "Internal Server Error" });
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

            //@todo testar novamente

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            if (allowAnonymous == false)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" },
                });
            }
            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement()
                        {
                             {
                                new OpenApiSecurityScheme
                                {
                                    Description = "Adds token to header",
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.Http,
                                    In = ParameterLocation.Header,
                                    Scheme = "bearer",
                                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                                },
                                new List<string>()
                            }
                    }
                };

            if (requiredScopes.Any())
            {
                if (requiredScopes.Any(a => a.UseApiGateway))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "x-api-key",
                        In = ParameterLocation.Header,
                        Description = "API Gateway access token",
                        Required = true,
                    });
                }
            }
        }
    }
}
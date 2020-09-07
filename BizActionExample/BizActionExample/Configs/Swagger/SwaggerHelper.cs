using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BizActionExample.Configs.Swagger
{
    public class SwaggerHelper
    {
        private static OpenApiInfo descricaoApi;
        public static Action<SwaggerGenOptions> ConfigureSwaggerGenOptions;

        public static void SetDescricaoBaseApi(OpenApiInfo descricao)
        {
            descricaoApi = descricao;
        }

        public static void ConfigureSwaggerGen(SwaggerGenOptions swaggerGenOptions)
        {
            var webApiAssembly = Assembly.GetEntryAssembly();
            ConfigureSwaggerGenOptions?.Invoke(swaggerGenOptions);

            AddSwaggerDocPerVersion(swaggerGenOptions, webApiAssembly, descricaoApi);
            ApplyDocInclusions(swaggerGenOptions);
            IncludeXmlComments(swaggerGenOptions);
        }

        private static void IncludeXmlComments(SwaggerGenOptions swaggerGenOptions)
        {
        }

        private static void ApplyDocInclusions(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.DocInclusionPredicate((docName, apiDesc) =>
            {
                apiDesc.TryGetMethodInfo(out MethodInfo methodInfo);

                var versions = methodInfo
                    .DeclaringType
                    .GetCustomAttribute<ApiVersionAttribute>()
                    .Versions;

                return versions.Any(v => $"v{v.ToString()}" == docName);
            });
        }

        private static void AddSwaggerDocPerVersion(SwaggerGenOptions swaggerGenOptions, Assembly webApiAssembly, OpenApiInfo descricaoApi = null)
        {
            var apiVersions = GetApiVersions(webApiAssembly);

            foreach (var apiVersion in apiVersions)
            {
                if (descricaoApi == null)
                {
                    descricaoApi = new OpenApiInfo
                    {
                        Title = "BizAction API",
                        Version = $"v{apiVersion}",
                        Description = @"API Rest",
                        Contact = new OpenApiContact
                        {
                            Email = "quangdung199697@gmail.com",
                            Url = new Uri("https://github.com/Quangdung1996")
                        }
                    };
                }

                swaggerGenOptions.SwaggerDoc($"v{apiVersion}", descricaoApi);
            }
        }

        private static IEnumerable<string> GetApiVersions(Assembly webApiAssembly)
        {
            var apiVersion = webApiAssembly.DefinedTypes
                .Where(x => x.IsSubclassOf(typeof(ControllerBase)) && x.GetCustomAttributes<ApiVersionAttribute>().Any())
                .Select(y => y.GetCustomAttribute<ApiVersionAttribute>())
                .SelectMany(v => v.Versions)
                .Distinct()
                .OrderBy(x => x);

            return apiVersion.Select(x => x.ToString());
        }

        public static void ConfigureSwagger(SwaggerOptions swaggerOptions)
        {
            swaggerOptions.RouteTemplate = "api-docs/{documentName}/swagger.json";
        }

        public static void ConfigureSwaggerUI(SwaggerUIOptions swaggerUIOptions)
        {
            var webApiAssembly = Assembly.GetEntryAssembly();
            var apiVersions = GetApiVersions(webApiAssembly);
            foreach (var apiVersion in apiVersions)
            {
                swaggerUIOptions.SwaggerEndpoint($"/api-docs/v{apiVersion}/swagger.json", $"V{apiVersion} Docs");
            }
            swaggerUIOptions.RoutePrefix = "api-docs";
        }

        #region properties

        public static string AwsHost { get; set; }
        public static string AwsBasePath { get; set; }

        #endregion properties
    }
}
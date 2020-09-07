using BizActionExample.Configs.Swagger;
using BizActionExample.Configs.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;

namespace BizActionExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();

            SwaggerHelper.ConfigureSwaggerGenOptions = swaggerGenOptions =>
            {
                swaggerGenOptions.OperationFilter<SwaggerParameterOperationFilter>();
                swaggerGenOptions.OperationFilter<ComplexRequestObjectOperationFilter>();
                swaggerGenOptions.OperationFilter<NonBodyParameterFilter>();
                swaggerGenOptions.OperationFilter<AuthorizeOperationFilter>();
                swaggerGenOptions.OperationFilter<AddResponseHeadersFilter>();
                swaggerGenOptions.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //Adding filter classes to Swagger for AWS
                //swaggerGenOptions.OperationFilter<Tools.Swagger.AspNetCore.Filters.ApiGateway.AwsApiGatewaySecurity>();
                //swaggerGenOptions.OperationFilter<Tools.Swagger.AspNetCore.Filters.ApiGateway.AwsApiGatewayIntegrationFilter>();
                //swaggerGenOptions.DocumentFilter<Tools.Swagger.AspNetCore.Filters.ApiGateway.AwsDocumentFilter>();

                //Adding parameter x-api-key to all methods, required for import for AWS
                //swaggerGenOptions.AddSecurityDefinition("api_key",
                //    new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme() { Type = "apiKey", Name = "x-api-key", In = "header" }
                //);
            };
            services.AddSwaggerGen(SwaggerHelper.ConfigureSwaggerGen);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseSwagger(SwaggerHelper.ConfigureSwagger);
            app.UseSwaggerUI(SwaggerHelper.ConfigureSwaggerUI);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
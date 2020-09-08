using BizActionExample.Configs.Swagger;
using BizActionExample.Configs.Swagger.Filters;
using BizActionExample.Controllers.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BizActionExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SwaggerHelper.SetDescricaoBaseApi(new OpenApiInfo()
            {
                Title = "BizActionExample",
                Description = "API BizActionExample"
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            SwaggerHelper.ConfigureSwaggerGenOptions = swaggerGenOptions =>
            {

                swaggerGenOptions.OperationFilter<SwaggerParameterOperationFilter>();
                swaggerGenOptions.OperationFilter<ComplexRequestObjectOperationFilter>();
                swaggerGenOptions.OperationFilter<NonBodyParameterFilter>();
                swaggerGenOptions.OperationFilter<AuthorizeOperationFilter>();
                swaggerGenOptions.OperationFilter<AddResponseHeadersFilter>();
                swaggerGenOptions.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                swaggerGenOptions.OperationFilter<RemoveVersionParameterFilter>();
                swaggerGenOptions.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
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
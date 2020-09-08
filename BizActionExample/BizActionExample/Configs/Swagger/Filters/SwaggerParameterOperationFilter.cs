using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace BizActionExample.Configs.Swagger.Filters
{
    public class SwaggerParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || operation.Parameters == null)
                return;

            var apiDescription = context.ApiDescription;

            var controllerActionDescriptor = apiDescription.GetProperty<ControllerActionDescriptor>();
            if (controllerActionDescriptor == null)
            {
                controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)
                {
                    apiDescription.SetProperty(controllerActionDescriptor);
                }
            }

            if (controllerActionDescriptor != null)
            {
                foreach (var parm in controllerActionDescriptor.MethodInfo.GetParameters())
                {
                    var swaggerParameter = parm.GetCustomAttributes(typeof(SwaggerParameterAttribute), true).FirstOrDefault();

                    if (swaggerParameter != null)
                    {
                        var operationParameter = operation.Parameters.FirstOrDefault(p => p.Name == parm.Name);
                        if (operationParameter != null)
                        {
                            var parameter = ((SwaggerParameterAttribute)swaggerParameter);

                            operationParameter.Description = parameter.Description;
                            operationParameter.Required = parameter.Required;
                        }
                    }
                }
            }
        }
    }
}
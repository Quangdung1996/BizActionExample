using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizActionExample.Configs.Swagger.Filters
{
	public class ComplexRequestObjectOperationFilter : IOperationFilter
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
				foreach (var parm in controllerActionDescriptor.MethodInfo.GetParameters().Where(pr => pr.ParameterType.BaseType == typeof(object)))
				{
					foreach (var p in parm.ParameterType.GetProperties())
					{
						//@todo falta fazer uma implementação para quando o parâmetro da request tem a tag [FromBody], ver exemplos no github do projeto Swashbuckle.AspNetCore.Examples

						var descriptionPr = p.GetCustomAttributes(typeof(DescriptionLocalized), true).FirstOrDefault();
						var operationParameter = operation.Parameters.FirstOrDefault(pr => pr.Name == p.Name);
						if (operationParameter != null && descriptionPr != null)
						{
							var parameter = ((DescriptionLocalized)descriptionPr);

							operationParameter.Description = parameter.Description;
							//operationParameter.Required = descriptionPr.Required;
						}
					}
				}
			}
		}
	}
}

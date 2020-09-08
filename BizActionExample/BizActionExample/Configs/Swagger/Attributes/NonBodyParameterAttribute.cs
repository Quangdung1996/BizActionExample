using Microsoft.OpenApi.Models;
using System;

namespace BizActionExample.Configs.Swagger.Attributes
{
    /// <summary>
    /// NonBodyParameter customizado.
    /// Pode ser usado para solicitar headers customizados, por exemplo: exigir que uma informação específica seja enviada nos headers de cada requisição.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class NonBodyParameterAttribute : Attribute
    {
        private readonly OpenApiParameter _parameter;

        public OpenApiParameter Parameter
        {
            get
            {
                return _parameter;
            }
        }

        /// <summary>
        /// Cria um NonBodyParameter customizado
        /// </summary>
        /// <param name="name">Nome do parameter, ex: x-api-key</param>
        /// <param name="description">Descrição do parameter ex: API Gateway access token</param>
        /// <param name="required">Se true o parâmetro é obrigatório</param>
        /// <param name="parameterType">Local do parametro, ex: header</param>
        public NonBodyParameterAttribute(string name, string description, bool required, ParameterLocation parameterType = ParameterLocation.Header)
        {
            _parameter = new OpenApiParameter
            {
                Name = name,
                Description = description,
                Required = required,
                In = parameterType
            };
        }

        /// <summary>
        /// Cria um NonBodyParameter customizado
        /// </summary>
        /// <param name="resourceType">Tipo de recurso de idioma</param>
        /// <param name="nameResourceName">Identificador do recurso de idioma para o name do parameter.</param>
        /// <param name="descriptionResourceName">Identificador do recurso de idioma para o description do parameter.</param>
        /// <param name="required">Se true o parâmetro é obrigatório</param>
        /// <param name="parameterType">Local do parametro, ex: header</param>
        public NonBodyParameterAttribute(Type resourceType, string nameResourceName, string descriptionResourceName, bool required, ParameterLocation parameterType = ParameterLocation.Header)
            : this(
                  new System.Resources.ResourceManager(resourceType).GetString(nameResourceName),
                  new System.Resources.ResourceManager(resourceType).GetString(descriptionResourceName),
                  required, parameterType)
        {
        }
    }
}
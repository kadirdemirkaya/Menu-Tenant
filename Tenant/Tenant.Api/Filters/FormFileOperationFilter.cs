using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tenant.Api.Filters
{
    public class FormFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParameters = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) ||
                            (p.ParameterType.IsGenericType && p.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<IFormFile>)))
                .ToList();

            if (formFileParameters.Count > 0)
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = formFileParameters.ToDictionary(p => p.Name, _ => new OpenApiSchema { Type = "string", Format = "binary" }),
                                Required = new HashSet<string>(formFileParameters.Select(p => p.Name)) // Burada HashSet kullanarak dönüştürme yapılıyor
                            }
                        }
                    }
                };
            }
        }
    }
}

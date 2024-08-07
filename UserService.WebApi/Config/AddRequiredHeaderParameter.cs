using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UserService.WebApi.Config;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }
        operation.Parameters.Add(
            new OpenApiParameter
            {
                Name = "x-Device",
                In = ParameterLocation.Header,
                Description = "Custom header for device",
                Required = false
            }
        );
    }
}

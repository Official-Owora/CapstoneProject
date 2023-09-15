using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CapstoneProject.WebApi.Extensions
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum = Enum.GetNames(context.Type)
                .Select(name => new OpenApiString(name))
                    .ToList<IOpenApiAny>();
                schema.Type = "string"; // Set the enum type to string
            }
        }
    }
}

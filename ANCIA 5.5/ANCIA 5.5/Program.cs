using System.Collections.Concurrent;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

ConcurrentDictionary<string, Fruit> _fruit = new ConcurrentDictionary<string, Fruit>();

RouteGroupBuilder fruitApi = app.MapGroup("/fruit");

fruitApi.MapGet("/", () => _fruit);

RouteGroupBuilder fruitApiWithValidation =
    fruitApi.MapGroup("/").AddEndpointFilterFactory(ValidationHelper.ValidateIdFactory);

fruitApiWithValidation.MapGet("{id}",
    (string id) => _fruit.TryGetValue(id, out var fruit) ? TypedResults.Ok(fruit) : Results.Problem(statusCode: 404));

fruitApiWithValidation.MapPost("{id}", (Fruit fruit, string id) => _fruit.TryAdd(id, fruit)
    ? TypedResults.Created($"/fruit/{id}", fruit)
    : Results.ValidationProblem(new Dictionary<string, string[]>
    {
        { "id", new[] { "A Fruit with this id already exists" } }
    }));


app.Run();

public record Fruit(string Name,int Stock);

class ValidationHelper
{
    internal static EndpointFilterDelegate ValidateIdFactory(EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        ParameterInfo[] parameters = context.MethodInfo.GetParameters();
        int? idPosition = null;

        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].Name == "id" && parameters[i].ParameterType == typeof(string))
            {
                idPosition = i;
                break;
            }
        }

        if (!idPosition.HasValue)
            return next;

        return async (invocationContext) =>
        {
            var id = invocationContext.GetArgument<string>(idPosition.Value);

            if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new[] { "Invalide format. Id must start with 'f'" } }
                });
            }

            return await next(invocationContext);
        };
    }
    
    internal static async ValueTask<object?> ValidateId(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        string id = context.GetArgument<string>(0);
        
        if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new[] { "Invalide format. Id must start with 'f'" } }
            });
        }

        return await next(context);
    }
}
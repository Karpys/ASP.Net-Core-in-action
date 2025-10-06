using System.Collections.Concurrent;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();


app.MapGet("/fruit", () => _fruit);

app.MapGet("/fruit/{id}",
    (string id) => _fruit.TryGetValue(id, out var fruit) ? TypedResults.Ok(fruit) : Results.Problem(statusCode:404));

app.MapPost("/fruit/{id}",
    (string id, Fruit fruit) => _fruit.TryAdd(id, fruit)
        ? TypedResults.Created($"/fruit/{id}", fruit)
        : Results.BadRequest(new
        {
            message = "Fruit already added", id
        }));

app.MapPut("/fruit/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();
});

app.MapDelete("/fruit/{id}", (string id) =>
{
    _fruit.TryRemove(id, out _);
    return Results.NoContent();
});

app.Run();

record Fruit(string Name,int Stock);
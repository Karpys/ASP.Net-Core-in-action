WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/fruit", () => Fruit.All);

Func<string, Fruit> getFruit = (string id) => Fruit.All[id];

app.MapGet("/fruit/{id}", getFruit);

app.MapPost("/fruit/{id}", Handlers.AddFruit);

Handlers handlers = new Handlers();
app.MapPut("/fruit/{id}", handlers.ReplaceFruit);

app.MapDelete("/fruit/{id}", DeleteFruit);

app.MapGet("/", () => "Hello World!");

app.Run();

void DeleteFruit(string id)
{
    Fruit.All.Remove(id);
}

class Handlers
{
    public void ReplaceFruit(string id, Fruit fruit)
    {
        Fruit.All[id] = fruit;
    }

    public static void AddFruit(string id, Fruit fruit)
    {
        Fruit.All.Add(id, fruit);
    }
}
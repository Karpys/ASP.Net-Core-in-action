WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DataContext>();
WebApplication app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    Console.WriteLine($"Retreived scope : {dbContext.RowCount}");
}

app.Run();

public class DataContext
{
    public int RowCount { get; } = Random.Shared.Next(1, 1000000);
}
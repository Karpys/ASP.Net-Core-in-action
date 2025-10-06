using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();


app.MapGet("/product/search", ([FromQuery(Name = "id")] int[] ids) => $"Received {ids.Length} ids");
app.MapPost("/product", (Product product) => $"Received {product}");

app.Run();

record Product(int Id,string Name,int Stock);
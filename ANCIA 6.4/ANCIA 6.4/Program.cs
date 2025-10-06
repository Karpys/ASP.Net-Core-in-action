WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(o =>
{
    o.LowercaseUrls = true;
    o.AppendTrailingSlash = true;
    o.LowercaseQueryStrings = false;
});

WebApplication app = builder.Build();

app.MapGet("/HealthCheck", () => Results.Ok()).WithName("healthcheck");
app.MapGet("/{name}", (string name) => name).WithName("product");

app.MapGet("", (LinkGenerator linkGen) => new []
{
    linkGen.GetPathByName("healthcheck"),
    linkGen.GetPathByName("product", new { name = "Big-Widget", Q = "Query1", Q2 = "Query2"})
});

app.Run();
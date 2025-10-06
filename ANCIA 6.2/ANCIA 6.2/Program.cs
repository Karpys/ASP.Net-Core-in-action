WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddRazorPages();
WebApplication app = builder.Build();

app.MapGet("/test", () => "Hello World!");
app.MapHealthChecks("/healthz");

app.Run();
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapPost("/sizes", (SizeDetails sizes) => $"Received {sizes}");

app.Run();

public record SizeDetails(double height, double width)
{
    public static async ValueTask<SizeDetails?> BindAsync(HttpContext context)
    {
        using StreamReader sr = new StreamReader(context.Request.Body);
        
        string? line1 = await sr.ReadLineAsync(context.RequestAborted);

        if (line1 is null)
        {
            return null;
        }
        
        string? line2 = await sr.ReadLineAsync(context.RequestAborted);

        if (line2 is null)
        {
            return null;
        }

        return double.TryParse(line1, out double height) && double.TryParse(line2, out double width)
            ? new SizeDetails(height, width)
            : null;
    }
}
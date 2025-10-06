WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/product/{id}", (ProductId id) => $"Received {id}!");

app.Run();

public record ProductId(int id)
{
    public static bool TryParse(string? s, out ProductId productId)
    {
        if(s is not null && s.StartsWith('p') && int.TryParse(s.AsSpan().Slice(1), out int id))
        {
            productId = new ProductId(id);
            return true;
        }

        productId = default;
        return false;
    }
}
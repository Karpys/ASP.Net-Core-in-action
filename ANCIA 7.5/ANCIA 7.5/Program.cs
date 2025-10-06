
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/stock/{id?}", (int? id) => $"Received {id}");
app.MapGet("/stock2", (int? id) => $"Received {id}");
app.MapGet("/stock", (Product? product) => $"Received {product}");
app.MapGet("/stock3", StockWithDefaultValue);

string StockWithDefaultValue(int id = 0)
{
    return $"Received {id}";
}

app.Run();

record Product(int Id):IParsable<Product>
{ 
    public static Product Parse(string s, IFormatProvider? provider)
    {
        return new Product(1);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Product result)
    {
        if (s is not null && s.StartsWith('p') && int.TryParse(s.AsSpan().Slice(1), out int id))
        {
            result = new Product(id);
            return true;
        }

        result = default;
        return false;
    }
};
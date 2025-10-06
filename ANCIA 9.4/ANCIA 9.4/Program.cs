WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<DataContext>();
builder.Services.AddTransient<Repository>();
WebApplication app = builder.Build();

app.MapGet("/", RowCounts);

app.Run();

string RowCounts(DataContext dataContext, Repository repository)
{
    return $"Data context : {dataContext.RowCount} , Repository : {repository.RowCount}";
}

public class DataContext
{
    public int RowCount { get; } = Random.Shared.Next(1, 1000000);
}

public class Repository
{
    private readonly DataContext _dataContext;

    public Repository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public int RowCount => _dataContext.RowCount;
}
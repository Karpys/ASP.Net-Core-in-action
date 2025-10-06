WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

List<Person> people = new List<Person>
{
    new ("Tom","Hanks"),
    new Person("Denzel", "Washington"),
    new ("Remi","Vedrenne"),
    new ("Lea","Lemaire"),
    new ("Leo","OphOph"),
    
};

app.MapGet("/person/{name}", (string name) => people.Where(p => p.firtName.StartsWith(name)));

app.Run();

public record Person(string firtName, string lastName);
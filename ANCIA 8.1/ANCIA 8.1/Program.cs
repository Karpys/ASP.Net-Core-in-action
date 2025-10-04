WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/register/{username}", RegisterUser);

string RegisterUser(string username)
{
    EmailSender emailSender = new EmailSender(new NetWorkClient(new EmailServerSettings()),new MessageFactory());
    emailSender.SendEmail(username);
    return $"Email sent to {username}";
}

app.Run();


public class EmailSender
{
    private readonly NetWorkClient _client;
    private readonly MessageFactory _factory;

    public EmailSender(NetWorkClient client, MessageFactory factory)
    {
        _client = client;
        _factory = factory;
    }
    public void SendEmail(string username)
    {
        var email = _factory.Create();
        _client.SendEmail(email);
        Console.WriteLine($"Email sent to {username}");
    }
}

public class MessageFactory
{
    public string Create()
    {
        return "email";
    }
}
public class NetWorkClient
{
    private readonly EmailServerSettings _settings;

    public NetWorkClient(EmailServerSettings settings)
    {
        _settings = settings;
    }

    public void SendEmail(string email)
    {
        Console.WriteLine($"Email sent");
    }
}

public class EmailServerSettings
{
    
}
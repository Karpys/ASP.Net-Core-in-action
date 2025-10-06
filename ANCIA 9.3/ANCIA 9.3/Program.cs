WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMessageSender,FacebookSender>();
builder.Services.AddScoped<IMessageSender,SmsSender>();
builder.Services.AddScoped<IMessageSender,EmailSender>();
WebApplication app = builder.Build();

app.MapGet("/register/{message}", SendMessage);
app.MapGet("/", () => "Navigate to register/{message} to test the Mock message sending");

app.Run();

string SendMessage(string message,IEnumerable<IMessageSender> sender)
{
    foreach (IMessageSender messageSender in sender)
    {
        messageSender.SendMessage(message);
    }
    
    return $"Message send : {message}";
}

public interface IMessageSender
{
    void SendMessage(string message);
}

public class FacebookSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Message send via Facebook : {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Message send via Sms : {message}");
    }
}

public class EmailSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Message send via email : {message}");
    }
}
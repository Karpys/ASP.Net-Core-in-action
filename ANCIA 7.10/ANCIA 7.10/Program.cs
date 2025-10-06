using System.ComponentModel.DataAnnotations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapPost("/users", (UserModel userModel) => userModel.ToString()).WithParameterValidation();
app.MapGet("/users/{id}", ([AsParameters] GetUserModel model) => $"Received {model.id}").WithParameterValidation();

app.Run();

struct GetUserModel
{
    [Range(0,100)]
    public int id { get; set; }
}

public record UserModel
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Your name")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}

public record CreateUserModel : IValidatableObject
{
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNumber))
        {
            yield return new ValidationResult("You must provide an Email or a Phone Number", new[] { nameof(Email), nameof(PhoneNumber) });
        }
    }
}
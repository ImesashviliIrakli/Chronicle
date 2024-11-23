namespace Chronicle.Application.Models.Identity;

public class UpdateProfileRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }   
    public required string UserName { get; set; }
    public required string PhoneNumber { get; set; }
}

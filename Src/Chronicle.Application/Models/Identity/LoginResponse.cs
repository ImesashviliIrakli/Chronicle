using System.ComponentModel.DataAnnotations;

namespace Chronicle.Application.Models.Identity;

public class LoginResponse
{
    public string Token { get; set; }
    public string UserId { get; set; }

    public LoginResponse(string token, string userId)
    {
        Token = token;
        UserId = userId;
    }
}

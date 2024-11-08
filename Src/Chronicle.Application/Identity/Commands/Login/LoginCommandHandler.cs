﻿using Chronicle.Application.Interfaces;
using Chronicle.Application.Options;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Chronicle.Application.Identity.Commands.Login;

public class LoginCommandHandler : ICommandQueryHandler<LoginCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtOptions _jwtOptions;
    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtOptions> jwtOptions
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions.Value;
    }
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);

        if (user is null)
            return Result.Failure(GlobalStatusCodes.NotFound, IdentityErrors.NotFound);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.password, false);

        if (result.Succeeded == false)
            return Result.Failure(GlobalStatusCodes.BadRequest, IdentityErrors.LoginFailed);

        JwtSecurityToken jwt = await GenerateTokenAsync(user);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        var resp = new LoginResponse(token, user.Id);

        return Result.Success(resp);
    }

    private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtOptions.Issuer,
        audience: _jwtOptions.Audience,
           claims: claims,
           expires: DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
           signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}

public class LoginResponse
{
    [Required]
    public string Token { get; set; }
    public string UserId { get; set; }

    public LoginResponse(string token, string userId)
    {
        Token = token;
        UserId = userId;
    }
}
using Application.DTOs.Auth;
using Application.Interfaces;
using AutoMapper;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services;

public class AuthenticationService
{
    private readonly ILoggerService _logger;
    private readonly CartService _cartService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private User? _user;

    public AuthenticationService(ILoggerService loggerService, UserManager<User> userManager
        , IMapper mapper, IConfiguration configuration, CartService cartService)
    {

        _logger = loggerService;
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
        _cartService = cartService;
    }

    public async Task<IdentityResult> Signup(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);

        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            await _cartService.InitCartAsync(user.Id);
        }

        return result;
    }
    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);

        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

        if (!result)
            _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong name or password.");

        return result;
    }
    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET").PadLeft(32, '0'));
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName),
                                       new Claim(ClaimTypes.NameIdentifier, _user.Id)
                                     };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}

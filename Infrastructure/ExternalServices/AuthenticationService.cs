using Application.Abstraction;
using Application.Abstraction.ExternalServices;
using Contracts.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.ExternalServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public string Login(LoginRequest request)
    {
        var user = _usuarioRepository.GetByEmailAndPassowrd(request);

        if (user == null)
        {
            return string.Empty;
        }

        var claims = new[]
        {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.Nombre)
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}

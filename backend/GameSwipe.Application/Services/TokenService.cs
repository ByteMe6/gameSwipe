using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using GameSwipe.Application.Interfaces.Services;
using GameSwipe.DataAccess.Entities.Users;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameSwipe.Application.Services;

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;

	public TokenService(IConfiguration config)
	{
		_config = config.GetSection("JWT");
	}

	public string GenerateToken(User user)
	{
		Roles role = user.Admin ? Roles.Admin : Roles.User;
		if(user.Username == "OWNER")
			role = Roles.Owner;
		Claim[] claims =
		[
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, role.ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
		];
		SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Key"] ?? throw new Exception("JWT Key not found")));
		SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		DateTime expDate = DateTime.Now.AddDays(int.Parse(_config["Lifetime"] ?? throw new Exception("Lifetime not found")));

		JwtSecurityToken token = new JwtSecurityToken(claims: claims, expires: expDate, signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public DateTime GetIssueDate(string token)
	{
		JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
		JwtSecurityToken jwt = handler.ReadJwtToken(token);
		return jwt.IssuedAt;
	}
}

public enum Roles
{
	Admin,
	User,
	Owner
}
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GameSwipe.Api.Extensions;

public static class JwtAuthentication
{
	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration jwtConfig)
	{
		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? throw new ArgumentNullException("jwtConfig:Key")));
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new()
					{
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						IssuerSigningKey = key,
						ClockSkew = TimeSpan.Zero
					};
				});
		return services;
	}
}

using Microsoft.OpenApi.Models;

namespace GameSwipe.Api.Extensions;

public static class SwaggerAuthentication
{
	public static IServiceCollection AddSwaggerAuthentication(this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Description =
					"JWT Authorization header using the Bearer scheme. " +
					"Example: \"Authorization: Bearer {token}\"",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
		});
		return services;
	}
}

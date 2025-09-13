using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Users;

public class UserLoginDto
{
	[Length(3, 200)]
	public string Identificator { get; set; }
	[Length(6, 32)]
	public string Password { get; set; }
	[Required]
	public IdentificationType Type { get; set; }

	public UserLoginDto(string identificator, string password, IdentificationType type)
	{
		Identificator = identificator;
		Password = password;
		Type = type;
	}
}
public enum IdentificationType
{
	Email,
	Username
}
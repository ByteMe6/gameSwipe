using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Users;

public class UserLoginDto
{
	[Length(3, 200)]
	public string Identificator { get; set; }
	[Length(8, 32)]
	public string Password { get; set; }
	[Required]
	public IdentificationType Type { get; set; }

	public UserLoginDto(string identification, string password, IdentificationType type)
	{
		Identificator = identification;
		Password = password;
		Type = type;
	}
}
public enum IdentificationType
{
	Email,
	Username
}
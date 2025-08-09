using System.ComponentModel.DataAnnotations;

namespace GameSwipe.Application.Dtos.Users;

public class UserLoginDto
{
	[Length(3, 200)]
	public string Identification { get; set; }
	[Length(8, 32)]
	public string Password { get; set; }

	public UserLoginDto(string identification, string password)
	{
		Identification = identification;
		Password = password;
	}
}

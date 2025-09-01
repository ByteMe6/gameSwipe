using GameSwipe.Application.Dtos.Users;

namespace GameSwipe.Application.Interfaces.Services.Users;

public interface IUserService
{
	public Task<bool> CreateUserAsync(UserWriteDto dto);

	public Task<bool> UpdateUserAsync(UserWriteDto dto);

	public Task<UserWriteDto> GetTemplateAsync();

	public Task<bool> DeleteUserAsync(long? id);

	public Task<bool> VerifyEmailAsync(string email, string code);

	public Task<bool> SendEmailVerificationAsync(string email);

	public Task<bool> ResetPasswordAsync(string email);

	public Task<string> LoginAsync(UserLoginDto dto);

	public Task<string> ReloginAsync(string token);

	//----

	public Task<UserGetFullDto> GetUserFullAsync(long? id);

	public Task<List<UserGetShortDto>> GetUsersByIdsAsync(List<long> ids);

	public Task<List<UserGetShortDto>> SearchUsersAsync(UserSearchDto dto, int page = 1, int pageSize = 20);
}

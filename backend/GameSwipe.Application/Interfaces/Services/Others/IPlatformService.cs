using GameSwipe.Application.Dtos.Platforms;
using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Interfaces.Services.Others;

public interface IPlatformService
{
	public Task<PlatformGetShortDto> GetPlatformShortAsync(long id);

	public Task<Platform> GetPlatformBareAsync(long id);

	public Task<bool> CreatePlatformAsync(PlatformWriteDto dto);

	public Task<bool> UpdatePlatformAsync(PlatformWriteDto dto);

	public Task<bool> DeletePlatformAsync(long id);
}

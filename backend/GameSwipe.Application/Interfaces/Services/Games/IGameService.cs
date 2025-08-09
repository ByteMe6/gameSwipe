using GameSwipe.Application.Dtos.Games;

namespace GameSwipe.Application.Interfaces.Services.Games;

public interface IGameService
{
	public Task<GameGetFullDto> GetGameFullAsync(long id);

	public Task<GameGetShortDto> GetGameShortAsync(long id);

	public Task<bool> CreateGameAsync(GameWriteDto dto);

	public Task<bool> DeleteGameAsync(long id);

	public Task<bool> UpdateGameAsync(GameWriteDto dto);
}

using GameSwipe.Application.Dtos.GameRecord;

namespace GameSwipe.Application.Interfaces.Services.Games;

public interface IGameRecordService
{
	public Task<GameRecordGetShortDto> GetGameRecordShortAsync(long id);

	public Task<List<GameRecordGetShortDto>> GetGameRecordsShortByUserAsync(long userId);

	public Task<bool> CreateGameRecordAsync(GameRecordWriteDto dto);

	public Task<bool> UpdateGameRecordAsync(GameRecordWriteDto dto);

	public Task<bool> DeleteGameRecordAsync(long id);
}

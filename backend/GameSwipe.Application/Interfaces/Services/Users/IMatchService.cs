using GameSwipe.Application.Dtos.Match;

namespace GameSwipe.Application.Interfaces.Services.Users;

public interface IMatchService
{
	public Task<bool> CreateMatchAsync(MatchWriteDto dto);

	public Task<bool> UpdateMatchAsync(MatchWriteDto dto);

	public Task<bool> DeleteMatchAsync(MatchWriteDto dto);

	public Task<List<MatchGetShortDto>> GetMatchesAllAsync(int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesAcceptedAsync(int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesDeclinedAsync(int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesReceivedAsync(int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesMutualAsync(int page = 1, int pageSize = 50);
}
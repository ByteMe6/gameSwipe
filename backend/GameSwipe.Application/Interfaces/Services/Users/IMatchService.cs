using GameSwipe.Application.Dtos.Matches;

namespace GameSwipe.Application.Interfaces.Services.Users;

public interface IMatchService
{
	public Task<bool> CreateMatchAsync(MatchWriteDto dto);

	public Task<bool> UpdateMatchAsync(MatchWriteDto dto);

	public Task<bool> DeleteMatchAsync(long id);

	public Task<List<MatchGetShortDto>> GetMatchesAllAsync(long? userId = null, int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesAcceptedAsync(long? userId = null, int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesDeclinedAsync(long? userId = null, int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesReceivedAsync(long? userId = null, int page = 1, int pageSize = 50);

	public Task<List<MatchGetShortDto>> GetMatchesMutualAsync(long? userId = null, int page = 1, int pageSize = 50);
}
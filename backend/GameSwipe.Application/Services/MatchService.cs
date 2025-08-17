
using GameSwipe.Application.Dtos.Matches;
using GameSwipe.Application.Dtos.Users;
using GameSwipe.Application.Interfaces.Services;
using GameSwipe.Application.Interfaces.Services.Users;
using GameSwipe.DataAccess.DbContexts;
using GameSwipe.DataAccess.Entities.Users;

using Microsoft.EntityFrameworkCore;


namespace GameSwipe.Application.Services;

public class MatchService : IMatchService
{
	private readonly GameSwipeDbContext _db;
	private readonly IIdentityService _id;

	public MatchService(GameSwipeDbContext db, IIdentityService id)
	{
		_db = db;
		_id = id;
	}

	public async Task<bool> CreateMatchAsync(MatchWriteDto dto)
	{
		User? target = await _db.Users.FindAsync(dto.TargetUserId);
		if(target is null)
			throw new Exception("Target not found");

		Match entity = new()
		{
			Status = dto.Status,
			Created = DateTime.UtcNow,
			TargetUserId = dto.TargetUserId,
			Updated = DateTime.UtcNow,
			UserId = _id.Id
		};

		_db.Matches.Add(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteMatchAsync(long id)
	{
		Match? entity = await _db.Matches.FindAsync(id);
		if(entity is null)
			throw new Exception("Match not found");

		if(entity.UserId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		_db.Matches.Remove(entity);
		return await _db.SaveChangesAsync() > 0;
	}

	public async Task<List<MatchGetShortDto>> GetMatchesAcceptedAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches
			.Where(x => x.UserId == userId)
			.Where(x => x.Status == MatchStatus.Accept)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesAllAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches
			.Where(x => x.UserId == userId || x.TargetUserId == userId)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesDeclinedAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;

		return await _db.Matches
			.Where(x => x.UserId == userId)
			.Where(x => x.Status == MatchStatus.Decline)
			.OrderBy(x => x.Id)
			.Skip(skip).Take(pageSize)
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public async Task<List<MatchGetShortDto>> GetMatchesMutualAsync(long? userId = null, int page = 1, int pageSize = 50)
	{
		if(userId is null)
			userId = _id.Id;

		if(userId != _id.Id && !_id.IsAdmin)
			throw new Exception("Access denied");

		int skip = (page - 1) * pageSize;





		var mutualIds = from m1 in _db.Matches
						join m2 in _db.Matches on new
						{
							UserId = m1.UserId,
							TargetUserId = m1.TargetUserId
						} equals new
						{
							UserId = m2.TargetUserId,
							TargetUserId = m2.UserId
						}
						where m1.UserId == userId && m1.Status == MatchStatus.Accept && m2.Status == MatchStatus.Accept
						select m1.Id;

		List<long> ids = await mutualIds.Skip(skip).Take(pageSize).ToListAsync();

		return await _db.Matches
			.Where(x => ids.Contains(x.Id))
			.Select(x => new MatchGetShortDto(
			x.Id,
			new UserGetShortDto(x.User.Id, x.User.Username, x.User.Name, x.User.Avatar),
			new UserGetShortDto(x.TargetUser.Id, x.TargetUser.Username, x.TargetUser.Name, x.TargetUser.Avatar)
			)).ToListAsync();
	}

	public Task<List<MatchGetShortDto>> GetMatchesReceivedAsync(int page = 1, int pageSize = 50)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateMatchAsync(MatchWriteDto dto)
	{
		throw new NotImplementedException();
	}
}

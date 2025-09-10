using GameSwipe.Application.Dtos.Matches;
using GameSwipe.Application.Interfaces.Services.Users;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("matches")]
public class MatchController : ControllerBase
{
	private readonly IMatchService _serv;
	public MatchController(IMatchService serv)
	{
		_serv = serv;
	}

	[HttpPost]
	public async Task<IActionResult> CreateMatchAsync(MatchWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreateMatchAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateMatchAsync(MatchWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdateMatchAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMatchAsync(long id)
	{
		try
		{
			return Ok(await _serv.DeleteMatchAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/all")]
	public async Task<IActionResult> GetMatchShortAsync(long userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.GetMatchesAllAsync(userId, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/accepted")]
	public async Task<IActionResult> GetMatchesAcceptedAsync(long userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.GetMatchesAcceptedAsync(userId, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/declined")]
	public async Task<IActionResult> GetMatchesDeclinedAsync(long userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.GetMatchesDeclinedAsync(userId, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/received")]
	public async Task<IActionResult> GetMatchesReceivedAsync(long userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.GetMatchesReceivedAsync(userId, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("user/{userId}/mutual")]
	public async Task<IActionResult> GetMatchesMutualAsync(long userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.GetMatchesMutualAsync(userId, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

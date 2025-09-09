using GameSwipe.Application.Dtos.Games;
using GameSwipe.Application.Interfaces.Services.Games;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("games")]
public class GameController : Controller
{
	private readonly IGameService _serv;

	public GameController(IGameService serv)
	{
		_serv = serv;
	}

	[HttpGet("{id}/short")]
	public async Task<IActionResult> GetGameShortAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetGameShortAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/full")]
	public async Task<IActionResult> GetGameFullAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetGameFullAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateGameAsync(GameWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreateGameAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteGameAsync(long id)
	{
		try
		{
			return Ok(await _serv.DeleteGameAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateGameAsync(GameWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdateGameAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

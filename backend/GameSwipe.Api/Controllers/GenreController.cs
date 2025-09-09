using GameSwipe.Application.Dtos.Genres;
using GameSwipe.Application.Interfaces.Services.Games;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("genres")]
public class GenreController : Controller
{
	private readonly IGenreService _serv;

	public GenreController(IGenreService serv)
	{
		_serv = serv;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetGenreShortAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetGenreShortAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/bare")]
	public async Task<IActionResult> GetGenreBareAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetGenreBareAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateGenreAsync(GenreWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreateGenreAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateGenreAsync(GenreWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdateGenreAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

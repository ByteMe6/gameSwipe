using GameSwipe.Application.Dtos.Languages;
using GameSwipe.Application.Interfaces.Services.Others;
using GameSwipe.DataAccess.Entities.Other;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("languages")]
public class LanguageController : Controller
{
	private readonly ILanguageService _serv;

	public LanguageController(ILanguageService serv)
	{
		_serv = serv;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetLanguageShortAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetLanguageShortAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/bare")]
	public async Task<IActionResult> GetLanguageBareAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetLanguageBareAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreateLanguageAsync(LanguageWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreateLanguageAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateLanguageAsync(LanguageWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdateLanguageAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteLanguageAsync(long id)
	{
		try
		{
			return Ok(await _serv.DeleteLanguageAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet]
	public async Task<IActionResult> GetLanguagesShortAsync()
	{
		try
		{
			return Ok(await _serv.GetLanguagesShortAsync());
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

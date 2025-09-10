using GameSwipe.Application.Dtos.Platforms;
using GameSwipe.Application.Interfaces.Services.Others;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("platforms")]
public class PlatformController : ControllerBase
{
	private readonly IPlatformService _serv;

	public PlatformController(IPlatformService serv)
	{
		_serv = serv;
	}

	[HttpGet]
	public async Task<IActionResult> GetPlatformsShortAsync()
	{
		try
		{
			return Ok(await _serv.GetPlatformsShortAsync());
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPlatformShortAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetPlatformShortAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}/bare")]
	public async Task<IActionResult> GetPlatformBareAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetPlatformBareAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreatePlatformAsync(PlatformWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreatePlatformAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdatePlatformAsync(PlatformWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdatePlatformAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePlatformAsync(long id)
	{
		try
		{
			return Ok(await _serv.DeletePlatformAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

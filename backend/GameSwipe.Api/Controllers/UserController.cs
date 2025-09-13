using GameSwipe.Application.Dtos.Users;
using GameSwipe.Application.Interfaces.Services.Users;

using Microsoft.AspNetCore.Mvc;

namespace GameSwipe.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
	private readonly IUserService _serv;
	public UserController(IUserService serv)
	{
		_serv = serv;
	}

	[HttpPost]
	public async Task<IActionResult> CreateUserAsync(UserWriteDto dto)
	{
		try
		{
			return Ok(await _serv.CreateUserAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut("update")]
	public async Task<IActionResult> UpdateUserAsync(UserWriteDto dto)
	{
		try
		{
			return Ok(await _serv.UpdateUserAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("update")]
	public async Task<IActionResult> GetTemplateAsync()
	{
		try
		{
			return Ok(await _serv.GetTemplateAsync());
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteUserAsync([FromQuery] long? id)
	{
		try
		{
			return Ok(await _serv.DeleteUserAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync(UserLoginDto dto)
	{
		try
		{
			return Ok(await _serv.LoginAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("refresh/{token}")]
	public async Task<IActionResult> RefreshUserAsync(string token)
	{
		try
		{
			return Ok(await _serv.ReloginAsync(token));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetUserAsync(long id)
	{
		try
		{
			return Ok(await _serv.GetUserFullAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("many/{ids}")]
	public async Task<IActionResult> GetUsersAsync(string ids)
	{
		try
		{
			List<long> realIds = ids.Split(",").Select(long.Parse).ToList();
			return Ok(await _serv.GetUsersByIdsAsync(realIds));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("search")]
	public async Task<IActionResult> SearchUsersAsync(UserSearchDto dto, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
	{
		try
		{
			return Ok(await _serv.SearchUsersAsync(dto, page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

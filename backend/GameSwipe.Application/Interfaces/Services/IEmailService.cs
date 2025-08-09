namespace GameSwipe.Application.Interfaces.Services;

public interface IEmailService
{
	public Task<bool> SendEmailAsync(string email, string subject, string message);
}

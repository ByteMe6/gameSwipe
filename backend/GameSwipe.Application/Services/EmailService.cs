
using GameSwipe.Application.Interfaces.Services;

namespace GameSwipe.Application.Services;

public class EmailService : IEmailService
{
	public Task<bool> SendEmailAsync(string email, string subject, string message)
	{
		Console.WriteLine($"Email to {email} with subject {subject} and message {message}");
		return Task.FromResult(true);
	}
}

using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.ServiceInterface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Kebab_Simulator.ApplicationServices.Services
{
	public class EmailsServices : IEmailsServices
	{
		private readonly IConfiguration _configuration;

		public EmailsServices(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void SendEmail(EmailDto dto)
		{
			var email = new MimeMessage();

			var emailSettings = _configuration.GetSection("EmailSettings");

			email.From.Add(MailboxAddress.Parse(emailSettings["EmailUserName"]));
			email.To.Add(MailboxAddress.Parse(dto.To));
			email.Subject = dto.Subject;
			var builder = new BodyBuilder
			{
				HtmlBody = dto.Body,
			};

			email.Body = builder.ToMessageBody();
			using var smtp = new SmtpClient();

			smtp.Connect(emailSettings["EmailHost"], 587, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate(emailSettings["EmailUserName"], emailSettings["EmailPassword"]);
			smtp.Send(email);
			smtp.Disconnect(true);
		}

		public void SendEmailToken(EmailTokenDto dto, string token)
		{
			dto.Token = token;
			var email = new MimeMessage();

			var emailSettings = _configuration.GetSection("EmailSettings");

			email.From.Add(MailboxAddress.Parse(emailSettings["EmailUserName"]));
			email.To.Add(MailboxAddress.Parse(dto.To));
			email.Subject = dto.Subject;
			var builder = new BodyBuilder
			{
				HtmlBody = dto.Body,
			};

			email.Body = builder.ToMessageBody();
			using var smtp = new SmtpClient();

			smtp.Connect(emailSettings["EmailHost"], 587, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate(emailSettings["EmailUserName"], emailSettings["EmailPassword"]);
			smtp.Send(email);
			smtp.Disconnect(true);
		}
	}
}
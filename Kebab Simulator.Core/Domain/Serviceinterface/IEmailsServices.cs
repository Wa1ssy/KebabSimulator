using Kebab_Simulator.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Core.ServiceInterface
{
	public interface IEmailsServices
	{
		void SendEmail(EmailDto dto);
		void SendEmailToken(EmailTokenDto dto, string token);
	}
}

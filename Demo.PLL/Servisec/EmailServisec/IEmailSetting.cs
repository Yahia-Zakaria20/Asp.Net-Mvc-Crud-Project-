using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Servisec.EmailServisec
{
	public interface IEmailSetting
	{
         Task SendEmailAsyn(string to,string Subject,string body );

      

    }
}

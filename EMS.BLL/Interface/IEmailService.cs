using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.Interface
{
    
        public interface IEmailService
        {
          public Task SendEmailAsync(string toName, string toEmail, string subject, string textContent);
        }
    
}

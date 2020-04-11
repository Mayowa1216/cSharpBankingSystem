using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystem.Core.Repositories
{
    public interface IEmailService
    {
        void SendEmail(string EmailTo, string user, string subject, string content);
    }
}

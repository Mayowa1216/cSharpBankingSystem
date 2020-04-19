using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystem.Core.Repositories
{
    public interface IUnitOfWork
    {
        IEmailService cs { get; }
        IAccount ct { get; }
        IAccountType cv { get; }
        void Complete();
    }
}

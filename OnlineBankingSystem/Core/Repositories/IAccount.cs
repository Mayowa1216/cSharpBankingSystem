using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystem.Core.Repositories
{
    public interface IAccount
    {
        void Add(Account acct);
        bool checkIfExist(string accountNo);
        Task<Account> Get(int id);
        Task<IEnumerable<AccountViewModel>> GetAll();
        void update(Account act);
        void Delete(Account act);
    }
}

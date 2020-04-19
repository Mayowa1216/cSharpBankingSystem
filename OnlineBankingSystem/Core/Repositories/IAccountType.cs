using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystem.Core.Repositories
{
    public interface IAccountType
    {
        void Add(AccountType acct);
        bool checkIfExist(string type);
        Task<AccountType> Get(int id);
        Task<IEnumerable<AccountTypeViewModel>> GetAll();
        void update(AccountType act);
    }
}

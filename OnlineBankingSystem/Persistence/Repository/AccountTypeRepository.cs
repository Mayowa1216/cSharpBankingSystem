using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.Repositories;
using OnlineBankingSystem.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OnlineBankingSystem.Persistence.Repository
{
    public class AccountTypeRepository : IAccountType
    {
        private ApplicationDbContext _context;
        public AccountTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(AccountType acct)
        {
            _context.accountType.Add(acct);
        }

        public bool checkIfExist(string type)
        {
            return _context.accountType.Any(x => x.AccountTypeName == type);
        }

        public async Task<AccountType> Get(int id)
        {
            return await _context.accountType.FindAsync(id);
        }

        public async Task<IEnumerable<AccountTypeViewModel>> GetAll()
        {
            return await (from c in _context.accountType
                          select new AccountTypeViewModel
                          {
                              name = c.AccountTypeName
                          }).ToListAsync();
        }

        public void update(AccountType act)
        {
            _context.Entry(act).State=EntityState.Modified;
        }
    }
}
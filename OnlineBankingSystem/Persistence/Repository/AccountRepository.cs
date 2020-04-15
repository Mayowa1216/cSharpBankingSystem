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
    public class AccountRepository : IAccount
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Account acct)
        {
            _context.account.Add(acct);
        }

        public bool checkIfExist(string accountNo)
        {
            var yup = _context.account.Any(x => x.ActNo == accountNo);
            return yup;
        }

        public void Delete(Account act)
        {
            _context.account.Remove(act);
        }

        public async Task<Account> Get(int id)
        {
            return await _context.account.FindAsync(id);
        }

        public async Task<IEnumerable<AccountViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void update(Account act)
        {
            _context.Entry(act).State = EntityState.Modified;
        }
    }
}
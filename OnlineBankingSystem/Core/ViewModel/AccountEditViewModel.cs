using OnlineBankingSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.ViewModel
{
    public class AccountEditViewModel
    {
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactLimit { get; set; }
        public string ActType { get; set; }
        public string user { get; set; }
        public IEnumerable<AccountTypeViewModel> type { get; set; }

    }
}
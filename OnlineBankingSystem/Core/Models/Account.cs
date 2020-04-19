using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.Models
{
    public class Account:Entity
    {
        public string ActNo { get; set; }
        public string AcctName { get; set; }
        public decimal Amount { get; set; }
        public decimal TransLimit { get; set; } = 100000M;
        public ICollection<AccountType> AccountType { get; set; }
        public ApplicationUser user { get; set; }
      
    }
}
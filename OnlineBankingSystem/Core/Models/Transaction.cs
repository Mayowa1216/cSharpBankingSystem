using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.Models
{
    public class Transaction:Entity
    {
        public string TransactionName { get; set; }
        public DateTime TansactionDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public ICollection<TransactionType> transactionType { get; set; }
        public ICollection<Account> accounts { get; set; }
    }
}
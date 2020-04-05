using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.Models
{
    public class TransactionType:Entity
    {
        public string TransactionTypeName { get; set; }
        public Transaction Transaction { get; set; }
    }
}
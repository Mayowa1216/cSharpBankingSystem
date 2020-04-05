using OnlineBankingSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Persistence.EntityConfigurations
{
    public class TransactionConfiguration:EntityTypeConfiguration<TransactionType>
    {
        public TransactionConfiguration()
        {
            HasRequired(x => x.Transaction)
                .WithMany(x => x.transactionType)
                .WillCascadeOnDelete(false);
                
        }
    }
}
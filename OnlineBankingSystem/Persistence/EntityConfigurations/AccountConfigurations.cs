using OnlineBankingSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Persistence.EntityConfigurations
{
    public class AccountConfigurations: EntityTypeConfiguration<AccountType>
    {
        public AccountConfigurations()
        {
            HasRequired(x => x.accounts)
                .WithMany(x => x.AccountType)
                .WillCascadeOnDelete(false);
        }
    }
}
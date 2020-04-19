using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.Models
{
    public class AccountType : Entity
    {
        public string AccountTypeName { get; set; }
        public Account accounts { get; set; }
        public bool isDelete { get; set; }


        public void Remove()
        {
            isDelete = true;
        }

    }
}
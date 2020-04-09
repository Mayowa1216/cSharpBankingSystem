using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.ViewModel
{
    public class RegisterVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
         public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
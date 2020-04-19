using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Core.ViewModel
{
    public class AccountTypeEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="invalid input")]
        public string name { get; set; }
    }
}
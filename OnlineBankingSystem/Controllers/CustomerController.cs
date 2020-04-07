using OnlineBankingSystem.Core.Infrastructure.Attributes;
using OnlineBankingSystem.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBankingSystem.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateGoogleCaptcha]
        public ActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            return RedirectToAction("", "");
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}
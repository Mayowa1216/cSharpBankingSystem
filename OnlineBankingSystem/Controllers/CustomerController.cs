using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineBankingSystem.Core;
using OnlineBankingSystem.Core.Infrastructure.Attributes;
using OnlineBankingSystem.Core.ViewModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineBankingSystem.Controllers
{
    public class CustomerController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CustomerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
          
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["message"] != null)
            {
                ViewBag.message = Session["message"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateGoogleCaptcha]
        public async Task<ActionResult> Index(RegisterViewModel model)
        {
            var user = UserManager.FindByEmail(model.Email);
          
            if (user == null)
            {
                var op = _mapper.Map<ApplicationUser>(model);
                op.UserName = model.Email;
              
                   
                    var result = await UserManager.CreateAsync(op, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(op, isPersistent: false, rememberBrowser: false);
                        //send confirmation message
                        string Id = op.Id;
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(Id);
                        //var confirmUrl = Url.Action("ConfirmEmail", "Account",
                           // new { userId = op.Id, token = code }, Request.Url.Scheme);


                       // _context.cs.SendEmail(op.Email, op.Lastname + op.Firstname, "Confirmation message", "Please confirm your account by clicking <a class='btn btn-primary' href=\"" + confirmUrl + "\">here</a>");

                        //await SignInManager.SignInAsync(op, isPersistent: false, rememberBrowser: false);
                        await UserManager.AddToRoleAsync(op.Id, "Customer");

                        Session["message"] = "Account successfully created, an email has been sent to your account for confirmation";
                        return RedirectToAction("Index", "Customer");
                    }
                    foreach (var j in result.Errors)
                    {
                        ModelState.AddModelError("", j.ToString());
                    }               
            }
            else
            {
                ViewBag.message = "Account successfully created, an email has been sent to your account for confirmation";
                //_context.cs.SendEmail(model.Email, model.Lastname + model.Firstname, "Account Exist", "Account Exist Please Login");
                //send message to them telling them  to login and that an account exist
            }

            return RedirectToAction("Index", "Customer");
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
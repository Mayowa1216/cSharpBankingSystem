using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineBankingSystem.Core;
using OnlineBankingSystem.Core.Infrastructure.Attributes;
using OnlineBankingSystem.Core.Repositories;
using OnlineBankingSystem.Core.ViewModel;
using System;
using System.Threading;
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
        private readonly IUnitOfWork unitOfWork;

        public CustomerController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.unitOfWork = unitOfWork;
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
                        var confirmUrl = Url.Action("ConfirmEmail", "Customer",
                            new { userId = op.Id, token = code }, Request.Url.Scheme);


                       unitOfWork.cs.SendEmail(op.Email, op.Lastname + op.Firstname, "Confirmation message", "Please confirm your account by clicking <a class='btn btn-primary' href=\"" + confirmUrl + "\">here</a>");

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
                unitOfWork.cs.SendEmail(model.Email, model.Lastname + model.Firstname, "Account Exist", "Account Exist Please Login");
                //send message to them telling them  to login and that an account exist
            }

            return RedirectToAction("Index", "Customer");
    }


        public ActionResult Login(string returnUrl)
        {
            if (Session["res"] != null)
            {
                ViewBag.res = Session["res"].ToString();
                Session["res"] = null;
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    await ResendEmailConfirmation(user.Id, "Email Confirmation");

                    ViewBag.errorMessage = "You must have a confirmed email to log on. "
                              + "The confirmation token has been resent to your email account.";
                    return View("error");
                }
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    Session["NextSleep"] = 0;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    var nextSleepMs = 0;
                    var nextSleep = Session["NextSleep"];
                    if (nextSleep == null)
                    {
                        Session["NextSleep"] = 500;
                    }
                    else
                    {
                        nextSleepMs = Convert.ToInt16(nextSleep);
                        Session["NextSleep"] = nextSleepMs * 2;
                    }

                    Thread.Sleep(nextSleepMs);
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return RedirectToAction("Login", "Customer");
            }
          
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (Session["Reset"] != null)
            {
                ViewBag.reset = Session["Reset"].ToString();
                Session["Reset"] = null;
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateGoogleCaptcha]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var resetUrl = Url.Action("ResetPassword", "Customer",
                           new { user.Email, token }, Request.Url.Scheme);


                    unitOfWork.cs.SendEmail(user.Email, user.Lastname, "Password reset message", "Please reset your account by clicking <a class='btn btn-primary' href=\"" + resetUrl + "\">here</a>");
                    ModelState.Clear();
                    Session["Reset"] = "Your password reset is in progress check your email for further details";
                }
                else
                {
                    unitOfWork.cs.SendEmail(user.Email, user.Lastname, "Reset password", "You don't have an account");
                    Session["Reset"] = "Your passsword reset is in progress check your email for further details";
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("ForgotPassword", "Customer");
        }


        [AllowAnonymous]
        public ActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordViewModel
            {
                Code = token,
                Email = email
            });
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }   
                        return View();
                    }
                    
                    ModelState.Clear();

                    Session["res"] = "A reset password was successfull";
                    unitOfWork.cs.SendEmail(user.Email, user.Lastname + user.Firstname, "Password Reset", "A reset password was successfull, if not you click the forgot password");
                    return RedirectToAction("Login", "Customer");
                }
            }
            ModelState.AddModelError("", "Invalid Request");

            return View();
        }


        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, token);
            return View(result.Succeeded ? "Login" : "Error");
        }


        public async Task<string> ResendEmailConfirmation(string userId, string subject)
        {
            var user = UserManager.FindById(userId);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Action("ConfirmEmail", "Customer",
               new { userId = userId, token = code }, protocol: Request.Url.Scheme);

             unitOfWork.cs.SendEmail(user.Email, user.Lastname + user.Firstname, "Resend Confirmation", "Please confirm your account by clicking <a class='btn btn-primary' href=\"" + callbackUrl + "\">here</a>");

            return callbackUrl;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
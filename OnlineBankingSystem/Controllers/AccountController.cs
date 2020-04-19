using OnlineBankingSystem.Core.Repositories;
using OnlineBankingSystem.Core.ViewModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineBankingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Account
        public async Task<ActionResult> CreateAccount()
        {
            var opo = new AccountEditViewModel();
            opo.type = await unitOfWork.ct.AllAct();
            return View(opo);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(AccountEditViewModel model)
        {
            return View();
        }  
        
    }
}
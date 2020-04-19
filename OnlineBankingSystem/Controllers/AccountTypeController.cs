using AutoMapper;
using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.Repositories;
using OnlineBankingSystem.Core.ViewModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineBankingSystem.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public AccountTypeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        // GET: AccountType
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AccountTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var opo = _mapper.Map<AccountType>(model);
                unitOfWork.cv.Add(opo);
                unitOfWork.Complete();

                return RedirectToAction("Index", "Customer");
            }
            return RedirectToAction("Index","Customer");
        }


        public async Task<ActionResult> ViewAccountType()
        {
            var opo =await unitOfWork.cv.GetAll();
            return View(opo);
        }

        public ActionResult Edit(int id)
        {
            var opo2 = unitOfWork.cv.Get(id);
            if (opo2 == null)
                return HttpNotFound();

            var opo3 = _mapper.Map<AccountTypeEditViewModel>(opo2);
            return View(opo3);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var opo = _mapper.Map<AccountType>(model);
                unitOfWork.cv.update(opo);
                unitOfWork.Complete();

                return RedirectToAction("ViewAccountType", "AccountType");
            }

            return View();
        }


    }
}
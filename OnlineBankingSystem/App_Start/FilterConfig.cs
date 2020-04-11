using OnlineBankingSystem.Core.Models;
using System.Web.Mvc;

namespace OnlineBankingSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomRequireHttpsFilter());
        }
    }
}

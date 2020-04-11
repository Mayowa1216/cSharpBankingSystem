using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBankingSystem.Core.Models
{
    public class CustomRequireHttpsFilter:RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {

            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)
                && !String.Equals(filterContext.HttpContext.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase))
            {
                base.HandleNonHttpsRequest(filterContext);
            }

           
            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;

            if (string.Equals(filterContext.HttpContext.Request.Url.Host, "localhost", StringComparison.OrdinalIgnoreCase))
            {
                
                url = "https://" + filterContext.HttpContext.Request.Url.Host + ":44301" + filterContext.HttpContext.Request.RawUrl;
            }

            filterContext.Result = new RedirectResult(url, true);
        }
    }
}
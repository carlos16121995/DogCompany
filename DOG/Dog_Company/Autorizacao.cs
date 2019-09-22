using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dog_Company
{
    public class Autorizacao : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //é disparado ante do action ser executado..

            foreach (var descr in context.ActionDescriptor.FilterDescriptors)
            {
                if (descr.Filter.GetType() == typeof(AllowAnonymousFilter))
                {
                    return;
                }
            }


            if (context.HttpContext.Session.GetString("logado") == null)
            {

                context.Result = new RedirectResult("~/Venda/Login");
            }

        }
    }
}

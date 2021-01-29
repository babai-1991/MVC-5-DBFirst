using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScratchPad.Filters
{
    public class MyActionFilter:FilterAttribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext has current controller name,current actionmethod name etc etc.
            filterContext.Controller.ViewBag.NoOfVisitors = 50;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.NoOfVisitors = 70;
        }
    }
}
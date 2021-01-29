using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScratchPad.Filters
{
    public class MyResultFilter:FilterAttribute,IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.NoOfVisitors = 100;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}
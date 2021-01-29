using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScratchPad.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string exceptionDetails = "==============================================================\n" +
                                      $"The exception Message {filterContext.Exception.Message} \n" +
                                      $"The Type of Exception {filterContext.Exception.GetType().ToString()} \n" +
                                      $"The exception source {filterContext.Exception.Source} \n" +
                                      "==============================================================\n";
            //Store inside a file
            StreamWriter writer = File.AppendText(filterContext.RequestContext.HttpContext.Request.PhysicalApplicationPath + "\\Errorlog.txt");
            writer.WriteLine(exceptionDetails);
            writer.Close();
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("~/error.html");
        }
    }
}
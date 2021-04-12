using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ScratchPad
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        private void Application_Error()
        {
            Exception exception = Server.GetLastError();
            //code for storing exception details.
            string message =
                String.Format(
                    $"Occured on {DateTime.Now.ToString("F")} Message: {exception.Message} , " +
                    $"Type: {exception.GetType().ToString()}, Source: {exception.Source}");
            StreamWriter writer = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + "\\Errorlog.txt");
            writer.WriteLine(message);
            writer.Close();

            Response.Redirect("~/error.html");
        }
    }
}

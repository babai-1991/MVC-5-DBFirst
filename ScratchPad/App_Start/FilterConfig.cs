using System;
using System.Web.Mvc;
using ScratchPad.Filters;

namespace ScratchPad
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filterCollection)
        {
            //filterCollection.Add(new ExceptionFilter());
            filterCollection.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception), // You could also specify specific exceptions
                View = "Error", //Error page name without extension, stored in Views/Shared is important
            });
        }
    }
}
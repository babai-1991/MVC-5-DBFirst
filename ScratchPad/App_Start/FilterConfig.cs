using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Filters;

namespace ScratchPad
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filterCollection)
        {
            filterCollection.Add(new ExceptionFilter());
        }
    }
}
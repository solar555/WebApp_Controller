using System.Web;
using System.Web.Mvc;
using WebApp_Controller.Filters;

namespace WebApp_Controller
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new EmployeeExceptionFilter());
        }
    }
}

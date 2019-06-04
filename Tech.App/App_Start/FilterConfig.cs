using System.Web;
using System.Web.Mvc;
using Tz.BackApp.Filters;

namespace Tech.App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
              filters.Add(new HandleErrorAttribute());
           // filters.Add(new BasicAuthenticationAttribute());
        }
    }
}

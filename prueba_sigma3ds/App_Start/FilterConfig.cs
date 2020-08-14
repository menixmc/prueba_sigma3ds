using System.Web;
using System.Web.Mvc;

namespace prueba_sigma3ds
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

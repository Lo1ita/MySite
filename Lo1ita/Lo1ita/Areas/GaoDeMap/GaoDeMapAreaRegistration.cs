using System.Web.Mvc;

namespace Lo1ita.Areas.GaoDeMap
{
    public class GaoDeMapAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GaoDeMap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GaoDeMap_default",
                "GaoDeMap/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
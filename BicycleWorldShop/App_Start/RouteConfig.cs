using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BicycleWorldShop.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
              name: "Preview",
              url: "{controller}/{action}/{id}",
              defaults: new
              {
                  controller = "Product",
                  action = "Preview",
                  id = UrlParameter.Optional
              }
          );
            routes.MapRoute(
           name: "Review",
           url: "{controller}/{action}/{id}",
           defaults: new
           {
               controller = "Review",
               action = "Create",
               id = UrlParameter.Optional
           }
       );
        }

    }
}
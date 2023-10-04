using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoAula3_MiguelHerazo_SamuelMeneses_JeronimoRivera
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Configura una ruta para la página de inicio
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            // Configura rutas para tu controlador IdeaDeNegocio
            routes.MapRoute(
                name: "IdeasConDesarrolloSostenible",
                url: "IdeaDeNegocio/IdeasConDesarrolloSostenible",
                defaults: new { controller = "IdeaDeNegocio", action = "IdeasConDesarrolloSostenible" }
            );

            // Agrega más rutas para tus otras acciones y controladores aquí

            // Ruta de fallback para manejar rutas no encontradas
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

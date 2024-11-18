using API.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*")
            {
                SupportsCredentials = true // Permitir el uso de credenciales
            };
            config.EnableCors();
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.MapHttpAttributeRoutes(); // Habilitar rutas basadas en atributos
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
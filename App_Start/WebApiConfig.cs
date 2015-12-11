using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SINF_EXAMPLE_WS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
               name: "InventoryValueApiRoute",
               routeTemplate: "api/{controller}/Value",
               defaults: new { id = RouteParameter.Optional, action = "GetValorInventario" }
            );

            config.Routes.MapHttpRoute(
               name: "CashApi",
               routeTemplate: "api/{controller}/Cash",
               defaults: new { id = RouteParameter.Optional, action = "GetCash" }
            );

            config.Routes.MapHttpRoute(
                name: "PaymentsApiRoute",
                routeTemplate: "api/{controller}/Salarios",
                defaults: new { controller = "Funcionarios", action = "GetSalarios" }
            );

            config.Routes.MapHttpRoute(
                name: "InvoicesApiRoute",
                routeTemplate: "api/{controller}/Documento",
                defaults: new { id = RouteParameter.Optional, action = "GetDocumento" }
            );

            config.Routes.MapHttpRoute(
                name: "VendasInfoRoute",
                routeTemplate: "api/{controller}/Info",
                defaults: new { id = RouteParameter.Optional, action = "GetInfo" }
            );

            config.Routes.MapHttpRoute(
                name: "TopApiRoute",
                routeTemplate: "api/{controller}/Top",
                defaults: new { id = RouteParameter.Optional, action = "GetTop" }
            );

            config.Routes.MapHttpRoute(
                name: "TotalApiRoute",
                routeTemplate: "api/{controller}/Total",
                defaults: new { id = RouteParameter.Optional, action = "GetTotal" }
            );
            config.Routes.MapHttpRoute(
                name: "TopClienteApi",
                routeTemplate: "api/{controller}/TopClient",
                defaults: new { id = RouteParameter.Optional, action = "GetTopClientes" }
            );

            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "Get" }
            );
            
            
            

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}

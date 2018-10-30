using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sbb_api.Model;
using sbb_api.Services;
using System.Web.Http;
namespace sbb_api
{
    public class Program
    {

        public static void Main(string[] args)
        {

            // Creating User Storage
            UserRepository UserRepository = UserRepository.Instance;

            //DBService.Instance.StoreUser(u);

            //DBService.Instance.LoadAllUsers();
            
            CreateWebHostBuilder(args).Build().Run();

           


        }

        public static void Register(HttpConfiguration config)
        {
            // New code
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

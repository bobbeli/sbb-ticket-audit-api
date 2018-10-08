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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

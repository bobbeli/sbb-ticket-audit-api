using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sbb_api.Services;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using sbb_api.Model;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sbb_api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GoogleController : Controller
    {
        [HttpPost]
        public IActionResult Auth([FromBody]User user)
        {
            // Set Up new User Account
            if ( UserRepository.Instance.Exist(user) )
            {
                // Load User From DB
                return Ok();

            } 
            else 
            {
                UserRepository.Instance.AddUser(user);
                return Ok();

            }

        }
    }
}

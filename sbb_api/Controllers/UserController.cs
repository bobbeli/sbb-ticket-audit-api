using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sbb_api.Model;
using sbb_api.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sbb_api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody]User user)
        {
            if (DBService.Instance.GetUser(user.Email) == null)
            {
                if ( DBService.Instance.CreateUser(user) )
                {
                    return Ok();
                }

                return NotFound("Nutzer konnte nicht erstellt werden.");
            }

            return NotFound("Nutzer exisitiert bereits.");
        }

        [HttpDelete]
        public void Delete(User user)
        {
        }

        [HttpPost]
        public IActionResult Get([FromBody]User user)
        {
            User u = DBService.Instance.GetUser(user.Email);
            if ( u != null)
            {
                return Ok(Json(u));
            }
            return NotFound("Nutzer exisitiert bereits.");
        }
    }
}

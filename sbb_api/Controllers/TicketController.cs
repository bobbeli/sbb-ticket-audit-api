using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using sbb_api.Model;
using sbb_api.Services;

using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sbb_api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TicketController : Controller
    {
        private TicketService TicketService;

        public TicketController()
        {
            this.TicketService = TicketService.Instance;
        }

        [HttpGet]
        public IActionResult GetTicket()
        {
            return Ok("Super Ticket");
        }


        [HttpPost]
        public IActionResult SyncWithMailServer([FromBody]User user)
        {
            if( UserRepository.Instance.Exist(user) )
            {
                if (TicketService.SyncMailsFromServer(user))
                {
                    return Ok();
                }

                return NotFound("Interner Serverfehler. ");
            }
            else
            {
                return NotFound("Nutzer " + user.Email + " existiert nicht.");
            }

        }

        [HttpPost]
        public IActionResult GetTickets([FromBody]User user)
        {
            if( UserRepository.Instance.Exist(user) )
            {
                Response.ContentType = "application/json";
                Response.StatusCode = 200;
                List<Ticket> p = DBService.Instance.GetTickets(user);
                return Ok(Json(p));
            }
            return NotFound("Nutzer " + user.Email + " existiert nicht.");
        }
    }
}

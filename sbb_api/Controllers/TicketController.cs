﻿using System;
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
    public class TicketController : Controller
    {
        private TicketService TicketService;

        public TicketController()
        {
            this.TicketService = TicketService.Instance;
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
                return Ok(Json(DBService.Instance.GetTickets(user)));
            }
            return NotFound("Nutzer " + user.Email + " existiert nicht.");
        }
    }
}

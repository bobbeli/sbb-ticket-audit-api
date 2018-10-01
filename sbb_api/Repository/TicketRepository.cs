using System;
using System.Collections.Generic;
using sbb_api.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using sbb_api.Helper;
using sbb_api.Services;

namespace sbb_api.Services
{
    public class TicketRepository
    {
        private List<Ticket> ticketList;

        public TicketRepository()
        {
            ticketList = new List<Ticket>();

        }

       
        public void addTicket(Ticket t)
        {
            this.ticketList.Add(t);
        }

        public int GetLength()
        {
            return this.ticketList.Count;
        }

        public List<Ticket> GetTickets()
        {
            return this.ticketList;
        }


    }
}

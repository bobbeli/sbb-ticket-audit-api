using System;
namespace sbb_api.Model
{
    public class Ticket
    {
        public enum TicketType { Regular, SuperSaver };

        public String Id { get; set; }

        public DateTime CreationDate { get; set; }

        public TicketType Type { get; set; }

        public double Price { get; set; }
    }
}

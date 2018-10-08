using System;
using LiteDB;

namespace sbb_api.Model
{
    public class Ticket
    {
        public Ticket(){
            _id = ObjectId.NewObjectId();
        }

        public enum TicketType { Regular, SuperSaver };

        public ObjectId _id { get; set; }

        public String _idMailProvider { get; set; }

        public DateTime CreationDate { get; set; }

        public TicketType Type { get; set; }

        public double Price { get; set; }
    }
}

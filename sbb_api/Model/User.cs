using System;
using LiteDB;
using sbb_api.Services;
using System.Collections.Generic;

namespace sbb_api.Model
{
    public class User
    {
        public User()
        {
            _id = ObjectId.NewObjectId();
            TicketList = new List<Ticket>();
        }

        public enum AccountType { Google, Outlook };

        public ObjectId _id { get; set;  }

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set;  }

        public AccountType EmailType { get; set;  }

        public GoogleService GmailService { get; set;  }

        public List<Ticket> TicketList { get; set;  }

    }
}

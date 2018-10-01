using System;
using sbb_api.Services;

namespace sbb_api.Model
{
    public class User
    {
        public User()
        {
            GmailService = new GoogleService();
            TicketRepository = new TicketRepository();
        }

        public enum AccountType { Google, Outlook };

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set;  }

        public AccountType EmailType { get; set;  }

        public GoogleService GmailService { get; set;  }

        public TicketRepository TicketRepository { get; set; }

    }
}

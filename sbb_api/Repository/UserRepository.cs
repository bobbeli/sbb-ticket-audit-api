using System;
using System.Collections.Generic;
using sbb_api.Model;

namespace sbb_api.Services
{
    public class UserRepository
    {
        List<User> userList;
        private static UserRepository instance = null;


        private UserRepository()
        {
            userList = new List<User>();
        }


        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserRepository();

                return instance;
            }
        }

        public void Update(User user)
        {
            User toUpdate = this.userList.Find((u) => u.Email == user.Email);
            toUpdate.FirstName = user.FirstName;
            toUpdate.LastName = user.LastName;
            toUpdate.GmailService = user.GmailService;
            toUpdate.TicketRepository = user.TicketRepository;
        }

        public void AddUser(User user)
        {
            this.userList.Add(user);
        }

        public bool Exist(User user)
        {
            return this.userList.Exists((u) => u.Email == user.Email);
        }

        public User GetUser(String email){
            return this.userList.Find((u) => u.Email.Equals(email));
        }

        public List<User> GetAll()
        {
            return this.userList;
        }
    }
}

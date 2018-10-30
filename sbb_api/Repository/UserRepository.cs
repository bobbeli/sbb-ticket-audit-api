using System;
using System.Collections.Generic;
using sbb_api.Model;
using System.Linq;

namespace sbb_api.Services
{
    public class UserRepository
    {
        List<User> userList;
        private static UserRepository instance = null;


        private UserRepository()
        {
            userList = new List<User>();
            userList = DBService.Instance.LoadAllUsers();
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
            User toUpdate = this.userList.Where((u) => u.Email == user.Email).First();
            toUpdate.FirstName = user.FirstName;
            toUpdate.LastName = user.LastName;
            toUpdate.GmailService = user.GmailService;
            toUpdate.TicketList = user.TicketList;
        }

        public void AddUser(User user)
        {
            this.userList.Add(user);
        }

        public bool Exist(User user)
        {
            if( user != null )
            {
                return this.userList.Exists((u) => u.Email == user.Email);
            }

            return false;
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

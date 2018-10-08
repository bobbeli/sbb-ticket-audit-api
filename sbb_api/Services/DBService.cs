using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections.Generic;
using LiteDB;
using System.Linq;

using sbb_api.Model;

namespace sbb_api.Services
{
    public class DBService
    {
        private static DBService instance;
        private String connectionString;

        private DBService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString;
        }

        public static DBService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBService();

                return instance;
            }
        }

        public List<Ticket> GetTickets(User user)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                List<Ticket> tickets = new List<Ticket>();
                var users = db.GetCollection<User>("users");

                try
                {
                    tickets = users.Find(u => u.Email == user.Email).First().TicketList;
                }
                catch (Exception e)
                {
                    // ToDo Logg exception
                    throw new Exception(e.Message);
                }

                return tickets;
            }
        }

        public bool UpdateUser(User NewUser)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var users = db.GetCollection<User>("users");

                User UpdatingUser = users.Find(u => u.Email == NewUser.Email).First();

                if( UpdatingUser != null ) {

                    UpdatingUser.TicketList = NewUser.TicketList;

                    // ToDo: Tickets werden nicht gespeichert, weshalb???
                    if (users.Update(UpdatingUser))
                    {
                        Console.WriteLine("User Updated " + UpdatingUser.Email);
                        return true;
                    }
                    return false;
                }
                return false;

            }
        }

        public bool CreateUser(User user)
        {

            using (var db = new LiteDatabase(connectionString))
            {
                var users = db.GetCollection<User>("users");

                try 
                {
                    user.GmailService = null;
                    users.Insert(user);

                }
                catch(Exception e)
                {
                    // ToDo Logg exception
                    throw new Exception(e.Message);
                }

                Console.WriteLine("New User created " + user.Email);

                return true;

            }
        }

        public User GetUser(String reqEmail)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var users = db.GetCollection<User>("users");

                IEnumerable<User> res = users.Find(u => u.Email == reqEmail);

                try
                {
                    return res.First();
                }
                catch(InvalidOperationException e)
                {
                    return null;
                }

            }
        }

        public List<User> LoadAllUsers()
        {
            using (var db = new LiteDatabase(connectionString)) 
            {
                var users = db.GetCollection<User>("users");

                return users.FindAll().ToList<User>(); 
            }
        }
    }
}

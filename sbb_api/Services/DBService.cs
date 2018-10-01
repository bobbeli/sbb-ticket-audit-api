using System;
using System.Collections.Generic;
using LiteDB;
using sbb_api.Model;

namespace sbb_api.Services
{
    public class DBService
    {
        private static DBService instance; 

        private DBService()
        {
           
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

        public static List<User> LoadUser()
        {
            return null;
        }

        public void StoreUser(User user)
        {

            List<User> userList;

            using (var db = new LiteDatabase(@"SBBApi.db"))
            {
                var users = db.GetCollection<List<User>>("users");

                UserRepository.Instance.AddUser(user);

                userList = UserRepository.Instance.GetAll();

                userList.ForEach((u) =>
                {
                    u.GmailService = null;

                });

                users.Insert(userList);
               
            }
        }

        public bool LoadAllUsers()
        {
            using(var db = new LiteDatabase(@"SBBApi.db")) 
            {
                var users = db.GetCollection<List<User>>("users");

                return true;
            }
        }

      }
}

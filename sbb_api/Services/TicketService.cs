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
    public class TicketService
    {
        private UserRepository UserReop;
        private static TicketService instance = null;
        private User user;

        private TicketService()
        {
            UserReop = UserRepository.Instance;
        }

        public static TicketService Instance
        {
            get
            {
                if (instance == null)
                    instance = new TicketService();

                return instance;
            }
        }


        public List<Ticket> LoadEmails(User RequestingUser)
        {

            this.user = UserReop.GetUser(RequestingUser.Email);

            if( this.user == null )
            {
                return null;
            }
            

            List<Message> messageList = this.LoadAllMessagesFromServer();


            messageList.ForEach((Message msg) =>
            {
                try
                {
                    String body = Base64Decoder.Decode(msg.Payload.Parts[0].Parts[0].Body.Data);
                    this.user.TicketRepository.addTicket(DeserializeTicket.Deserialize(body));

                }
                catch (FormatException e)
                {
                    //ToDo catch Deserialize Exception and inform Client
                    Console.WriteLine(e.Message);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }

            });

            this.UserReop.Update(this.user);

            return this.user.TicketRepository.GetTickets();
        }

        public List<Message> LoadAllMessagesFromServer()
        {
            List<Message> messageIds = GetAllMessageIds();
            List<Message> emailList = new List<Message>();


            messageIds.ForEach((msg) =>
            {
                emailList.Add(getMessage(msg.Id));
            });

            return emailList;
        }

        public List<Message> GetAllMessageIds()
        {
            List<Message> result = new List<Message>();

            String query = "from: sbbclient@sbb.ch";

            UsersResource.MessagesResource.ListRequest request = this.user.GmailService.GmailService.Users.Messages.List(this.user.Email);

            request.Q = query;

            do
            {
                try
                {
                    ListMessagesResponse response = request.Execute();
                    result.AddRange(response.Messages);
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            return result;
        }

        public Message getMessage(String messageId)
        {
            try
            {
                return user.GmailService.GmailService.Users.Messages.Get(this.user.Email, messageId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured " + e.Message);
            }

            return null;
        }


    }
}

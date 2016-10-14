using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;

namespace JoinAndDo.Hubs
{
    public class ChatHub : Hub
    {
        private SqlRepository _sqlRepository = new SqlRepository();
        static List<User> Users = new List<User>();

        public void SendMsgToAccession(string name, string hash, string message, string idAccession)
        {
            int res;
            if( int.TryParse(idAccession, out res) )
            {
                if( _sqlRepository.SendMsgToAccession(name, hash, res, message) == "Ok" )
                {
                    Clients.Group(idAccession).addMessageToAccession(name, message);
                }
            }
            
        }
        public void SendMsgToUser(string name, string hash, string message, string toUser)
        {
            if (_sqlRepository.SendMsg(name, hash, toUser, message) == "Ok")
            {
                var user = Users.Find(u => u.Login == toUser);
                Clients.Client(user.Id).addMessage(name, message);
            }

        }
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.Id == id))
            {
                Users.Add(new User { Id = id, Login = userName });
            }
        }
        public void ConnectToAccession(string userName, string idAccession)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.Id == id))
            {
                Users.Add(new User { Id = id, Login = userName });

                JoinRoom(userName, idAccession);
            }
        }
        public void ConnectToDialog(string userName, string toUser)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.Id == id))
            {
                Users.Add(new User { Id = id, Login = userName });

            }
            JoinRoom(userName, CreateIdOfDialog(userName, toUser));
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.Id == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Login);
            }

            return base.OnDisconnected(stopCalled);
        }
        public System.Threading.Tasks.Task JoinRoom( string idUser, string idAccession)
        {
            return Groups.Add(Context.ConnectionId, idAccession);
        }
        public System.Threading.Tasks.Task LeaveRoom(string idUser, string idAccession)
        {
            return Groups.Remove(Context.ConnectionId, idAccession);
        }
        public string CreateIdOfDialog(string user1, string user2)
        {
            string[] arr = { user1, user2 };
            Array.Sort(arr);
            return arr[0] + arr[1];
        }
    }
}
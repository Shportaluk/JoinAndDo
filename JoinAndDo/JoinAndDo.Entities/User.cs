using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class User
    {
        public string id;
        public string login;
        public string firstName;
        public string lastName;
        public bool isOnline;
        public string hash;
        public string fulfillmentAccession;
        public string acceptedConnections;
        public string timeWorking;

        public User()
        {
            
        }
    }
}

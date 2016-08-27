using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public string Hash { get; set; }
        public string FulfillmentAccession { get; set; }
        public string AcceptedConnections { get; set; }
        public string TimeWorking { get; set; }
        public string Role { get; set; }
        public string PathImg { get; set; }
        public string PathImgMini { get; set; }

        public User()
        {
            
        }
    }
}

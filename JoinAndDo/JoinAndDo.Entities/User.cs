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
        public string Role { get; set; }
        public string PathImg { get; set; }
        public string PathImgMini { get; set; }

        public int CompletedAccessions { get; set; }
        public int AbandonedAccessions { get; set; }
        public int CurrentlyAccessions { get; set; }
        public int AllAccessions { get; set; }

        public string NewMsg { get; set; }

        public int CompletedAccessionsPercent { get; set; }
        public int AbandonedAccessionsPercent { get; set; }
        public int CurrentlyAccessionsPercent { get; set; }

        public User()
        {
            CompletedAccessionsPercent = 0;
            AbandonedAccessionsPercent = 0;
            CurrentlyAccessionsPercent = 0;
        }
    }
}

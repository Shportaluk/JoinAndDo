using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class Interlocutor
    {
        public string login;
        public List<Message> dialog;

        public Interlocutor() { }
        public Interlocutor( string login, List<Message> dialog)
        {
            this.login = login;
            this.dialog = dialog;
        }
    }
}

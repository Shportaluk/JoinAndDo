using System.Collections.Generic;

namespace JoinAndDo.Entities
{
    public class Interlocutor
    {
        public string login;
        public List<Message> dialog;
        
        public Interlocutor() { }
        public Interlocutor( string login, List<Message> dialog )
        {
            this.login = login;
            this.dialog = dialog;
        }
    }
}

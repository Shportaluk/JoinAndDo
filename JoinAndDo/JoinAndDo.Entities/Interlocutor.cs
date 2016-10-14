using System.Collections.Generic;

namespace JoinAndDo.Entities
{
    public class Interlocutor
    {
        public string Login { get; set; }
        public List<Message> Dialog { get; set; }
        
        public Interlocutor() { }
        public Interlocutor( string login, List<Message> dialog )
        {
            this.Login = login;
            this.Dialog = dialog;
        }
    }
}

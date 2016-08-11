using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class Accession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public int People { get; set; }
        public int AllPeople { get; set; }
        public List<string> ListAvailableRoles { get; set; }

        public Accession() { }
    }
}

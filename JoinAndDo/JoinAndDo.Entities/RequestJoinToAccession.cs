using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class RequestJoinToAccession
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string ToIdAccession { get; set; }
        public string Status { get; set; }

        public RequestJoinToAccession() { }
    }
}

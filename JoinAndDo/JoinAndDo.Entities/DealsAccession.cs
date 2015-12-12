using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class DealsAccession : JoinsEntity
    {
        public string User { get; set; }

        public DealsAccession()
        {
            this.Title = "";
            this.Text = "";
            this.People = 0;
            this.AllPeople = 0;
            this.User = "";
        }

        public DealsAccession( string title, string text, string user, int people, int allPeople )
        {
            this.Title = title;
            this.Text = text;
            this.User = user;
            this.People = people;
            this.AllPeople = allPeople;
        }
    }
}

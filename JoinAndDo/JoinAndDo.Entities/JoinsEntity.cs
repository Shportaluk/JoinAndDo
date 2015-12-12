using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class JoinsEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int People { get; set; }
        public int AllPeople { get; set; }

        public JoinsEntity(  )
        {
            this.Title = "";
            this.Text = "";
            this.People = 0;
            this.AllPeople = 0;
        }

        public JoinsEntity( string title, string text, int people, int allPeople )
        {
            this.Title = title;
            this.Text = text;
            this.People = people;
            this.AllPeople = allPeople;
        }
    }
}

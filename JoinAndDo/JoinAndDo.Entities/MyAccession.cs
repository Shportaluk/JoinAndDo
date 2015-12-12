using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class MyAccession : JoinsEntity
    {
        public bool IsComplete { get; set; }

        public MyAccession()
        {
            this.Title = "";
            this.Text = "";
            this.People = 0;
            this.AllPeople = 0;
            this.IsComplete = false;
        }

        public MyAccession( string title, string text, int people, int allPeople, bool isComplete )
        {
            this.Title = title;
            this.Text = text;
            this.People = people;
            this.AllPeople = allPeople;
            this.IsComplete = isComplete;
        }
    }
}

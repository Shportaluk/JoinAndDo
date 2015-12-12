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
            this.IsComplete = false;
        }

        public MyAccession( string title, string text, bool isComplete )
        {
            this.Title = title;
            this.Text = text;
            this.IsComplete = isComplete;
        }
    }
}

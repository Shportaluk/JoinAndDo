using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class DealsAccession
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public DealsAccession()
        {
            this.Title = "";
            this.Text = "";
        }

        public DealsAccession( string title, string text )
        {
            this.Title = title;
            this.Text = text;
        }
    }
}

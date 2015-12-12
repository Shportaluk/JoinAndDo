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

        public JoinsEntity(  )
        {
            this.Title = "";
            this.Text = "";
        }

        public JoinsEntity( string title, string text )
        {
            this.Title = title;
            this.Text = text;
        }
    }
}

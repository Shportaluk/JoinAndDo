﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Entities
{
    public class My_accession : JoinsEntity
    {
        public bool IsComplete { get; set; }

        public My_accession()
        {
            this.Title = "";
            this.Text = "";
            this.IsComplete = false;
        }

        public My_accession( string title, string text, bool isComplete )
        {
            this.Title = title;
            this.Text = text;
            this.IsComplete = isComplete;
        }
    }
}

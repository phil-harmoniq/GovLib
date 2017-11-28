using System;
using System.Collections.Generic;
using System.Text;
using Gov.NET.Common.Models.Contracts;

namespace Gov.NET.Models
{
    public class Senator : Politician, ISenator
    {
        public string Rank { get; set; }
        public int Class { get; set; }

        public override string ToString()
        {
            return $"Senator {FullName} ({Party}) [{State}]";
        }
    }
}

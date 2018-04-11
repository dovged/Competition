using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompKidsKKTModel
    {
        public int CompId { get; set; }
        public string CompName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Boolean Register { get; set; } 
    }
}
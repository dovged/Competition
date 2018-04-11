using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompKidsClimbModel
    {
        public int CompId { get; set; }
        public string CompName { get; set; }
        public int ClimberId { get; set; }
        public string ClimberName { get; set; }
        public Boolean Register { get; set; }
    }
}
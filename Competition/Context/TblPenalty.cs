using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblPenalty")]
    public class TblPenalty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
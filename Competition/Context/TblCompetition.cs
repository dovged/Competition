using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblCompetition")]
    public class TblCompetition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }
        public bool Open { get; set; }
    }
}
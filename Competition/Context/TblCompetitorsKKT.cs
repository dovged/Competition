using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblCompetitorsKKT")]
    public class TblCompetitorsKKT
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public int TeamId { get; set; }
        public string Group { get; set; }
        public Boolean Paid { get; set; }
    }
}
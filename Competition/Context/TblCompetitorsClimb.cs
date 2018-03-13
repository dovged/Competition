using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblCompetitorsClimb")]
    public class TblCompetitorsClimb
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public int UserId { get; set; }
        public int Group { get; set; }
        public Boolean Paid { get; set; }
        public Boolean Tag { get; set; }
    }
}
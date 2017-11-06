using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblCompTeam")]
    public class TblCompTeam
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public int TeamId { get; set; }
    }
}
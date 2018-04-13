using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblRouteKKT")]
    public class TblRouteKKT
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public int Points { get; set; }
        public int CompetitionId { get; set; }
        public string Type { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblRoute")]
    public class TblRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MaxTime { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
        public int CompetitionId { get; set; }
    }
}
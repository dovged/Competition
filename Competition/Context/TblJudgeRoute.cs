using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblJudgeRouteClimb")]
    public class TblJudgeRoute
    {
        public int Id { get; set; }
        public int JudgeId { get; set; }
        public int RouteId { get; set; }
    }
}
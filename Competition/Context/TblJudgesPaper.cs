using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblJudgesPaper")]
    public class TblJudgesPaper
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public int TeamId { get; set; }
    }
}
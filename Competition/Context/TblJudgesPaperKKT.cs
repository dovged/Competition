using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblJudgesPaperKKT")]
    public class TblJudgesPaperKKT
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public int TeamId { get; set; }
        public int TypeId { get; set; }
        public DateTime Date { get; set; }
        public int JudgeId { get; set; }
    }
}
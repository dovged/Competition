using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblJudgesPaperClim")]
    public class TblJudgesPaperClim
    {
        public int Id { get; set; }
        public int TopAttempt { get; set; }
        public int BonusAttemot { get; set; }
        public DateTime Date { get; set; }
        public int JudgeId { get; set; }
        public int RouteId { get; set; }
        public int ClimberId { get; set; }
        public int TypeId { get; set; }
    }
}
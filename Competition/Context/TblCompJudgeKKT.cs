using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblCompJudgeKKT")]
    public class TblCompJudgeKKT
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }

    }
}
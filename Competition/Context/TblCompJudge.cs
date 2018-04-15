using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblCompJudgeClimb")]
    public class TblCompJudge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
    }
}
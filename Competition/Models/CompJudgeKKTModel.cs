using Competition.Context;

namespace Competition.Models
{
    public class CompJudgeKKTModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
        //Teisėjo vardas ir pavardė string formatu
        public string JudgeName { get; set; }

        public CompJudgeKKTModel(TblCompJudgeKKT row)
        {
            Id = row.Id;
            UserId = row.UserId;
            CompId = row.CompId;
        }
    }
}
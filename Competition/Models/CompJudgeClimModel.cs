using Competition.Context;
using System.Collections.Generic;

namespace Competition.Models
{
    public class CompJudgeClimModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
        public string Group { get; set; }

        //Teisėjo vardas ir pavardė string formatu
        public string JudgeName { get; set; }
        public List<JudgeRouteModel> Routes { get; set; }

        public CompJudgeClimModel(TblCompJudgeClim row)
        {
            Id = row.Id;
            UserId = row.UserId;
            CompId = row.CompId;
            Group = row.Group;
        }
    }
}
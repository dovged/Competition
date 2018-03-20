using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class JudgesPaperClimbModel
    {
        public int Id { get; set; }
        public int TopAttempt { get; set; }
        public int BonusAttempt { get; set; }
        public DateTime Date { get; set; }
        public int JudgeId { get; set; }
        // Teisėjo vardas ir pavardė string formatu;
        public string Judge { get; set; }
        public int ClimberId { get; set; }
        // Laipiotojo vardas ir pavardė string formatu;
        public string Climber { get; set; }
        public int PaperTypeId { get; set; }
        public string TypeName { get; set; }
        public int RouteId { get; set; }
        public int RouteNumber { get; set; }

        public JudgesPaperClimbModel() { }
        public JudgesPaperClimbModel(TblJudgesPaperClim row)
        {
            Id = row.Id;
            TopAttempt = row.TopAttempt;
            BonusAttempt = row.BonusAttempt;
            Date = row.Date;
            PaperTypeId = row.TypeId;
            RouteId = row.RouteId;
        }
    }
}
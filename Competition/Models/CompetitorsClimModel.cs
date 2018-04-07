using Competition.Context;
using System;

namespace Competition.Models
{
    public class CompetitorsClimModel
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int ClimberId { get; set; }
        public string Group { get; set; }
        public bool Paid { get; set; }
        // Laipiotojo Vardas ir Pavardė string formatu
        public string ClimberName { get; set; }
        public string CompetitionName { get; set; }
        public string Club { get; set; }
        public DateTime Date { get; set; }
        public Boolean Update { get; set; }

        public CompetitorsClimModel(TblCompetitorsClimb row)
        {
            Id = row.Id;
            CompId = row.CompetitionId;
            ClimberId = row.UserId;
            Group = row.Group;
            Paid = row.Paid;
        }
    }
}
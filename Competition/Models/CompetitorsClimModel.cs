using Competition.Context;

namespace Competition.Models
{
    public class CompetitorsClimModel
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int ClimberId { get; set; }
        // Laipiotojo Vardas ir Pavardė string formatu
        public string ClimberName { get; set; }

        public CompetitorsClimModel(TblCompetitorsClimb row)
        {
            Id = row.Id;
            CompId = row.CompetitionId;
            ClimberId = row.UserId;
        }
    }
}
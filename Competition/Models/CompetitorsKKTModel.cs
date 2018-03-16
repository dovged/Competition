using Competition.Context;

namespace Competition.Models
{
    public class CompetitorsKKTModel
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int TeamId { get; set; }
        public string Group { get; set; }
        public bool Paid { get; set; }
        public string TeamName { get; set; }
        

        public CompetitorsKKTModel(TblCompetitorsKKT row)
        {
            Id = row.Id;
            CompId = row.CompetitionId;
            TeamId = row.TeamId;
            Group = row.Group;
            Paid = row.Paid;
        }
    }
}
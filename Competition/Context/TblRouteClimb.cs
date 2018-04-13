using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblRouteClim")]
    public class TblRouteClimb
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int PointsBonus { get; set; }
        public int PointsTop { get; set; }
        public int PointsFlash { get; set; }
        public int CompetitionId { get; set; }
        public string Type { get; set; }
    }
}
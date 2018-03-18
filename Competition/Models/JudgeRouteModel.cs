using Competition.Context;

namespace Competition.Models
{
    public class JudgeRouteModel
    {
        public int Id { get; set; }
        public int JudgeId { get; set; }
        public int RouteId { get; set; }

        public int RouteNumber { get; set; }

        public JudgeRouteModel(TblJudgeRoute row)
        {
            Id = row.Id;
            JudgeId = row.JudgeId;
            RouteId = row.RouteId;
        }
    }
}
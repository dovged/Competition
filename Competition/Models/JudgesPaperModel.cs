using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class JudgesPaperModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int RouteId { get; set; }
        public string TeamName { get; set; }
        public string RouteName { get; set; }
        public string Time { get; set; }
        public string TimewithPenalty { get; set; }
        public int PenaltySum { get; set; }
        public int Points { get; set; }
        public List<PenaltyQuantityModel> Penalties { get; set; }

        public JudgesPaperModel()
        {
            Time = "00:00:00";
            TimewithPenalty = "00:00:00";
            Points = 0;
        }

        public JudgesPaperModel(TblJudgesPaper row)
        {
            Id = row.Id;
            RouteId = row.RouteId;
            TeamId = row.TeamId;
            Time = row.Time;
        }

        public void SetPenalties(List<PenaltyQuantityModel> penalties)
        {
            Penalties = penalties;
        }

        public void SetTimeWithPenalty(string time)
        {
            TimewithPenalty = time;
        }

        public void SetPoints(int points)
        {
            Points = points;
        }

        public void SetTime(string time)
        {
            Time = time;
        }

        public void SetPenalySum(int sum)
        {
            PenaltySum = sum;
        }

        public void SetRouteName(string name)
        {
            RouteName = name;
        }

        public void SetTeamName(string name)
        {
            TeamName = name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class ResultModel
    {
        public int Place { get; set; }
        public string TeamName { get; set; }
        public string TimeSum { get; set; }
        public int PointsSum { get; set; }
        public List<JudgesPaperKKTModel> JudgesPapers { get; set; }

        public ResultModel()
        {
            Place = 0;
            TeamName = "TeamName";
            TimeSum = "00:00:00";
            PointsSum = 0;
            JudgesPapers = null;
        }

        public void PlusPoints(int points)
        {
            PointsSum += points;
        }

        public void MinusPoints(int points)
        {
            PointsSum -= points;
        }
    }
}
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
        public List<JudgesPaperModel> judgesPapers { get; set; }

        public ResultModel()
        {
            Place = 0;
            TeamName = "TeamName";
            TimeSum = "00:00:00";
            PointsSum = 0;
            judgesPapers = null;
        }

        public void SetTeamName(string Name)
        {
            TeamName = Name;
        }

        public void SetTime(string time)
        {
            TimeSum = time;
        }

        public void PlusPoints(int points)
        {
            PointsSum += points;
        }

        public void SetJudgesPapers(List<JudgesPaperModel> papers)
        {
            judgesPapers = papers;
        }
    }
}
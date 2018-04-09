using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class ResultsClim
    {
        public int Place { get; set; }
        public string Climber { get; set; }
        public int TopNumber { get; set; }
        public int BonusNumber { get; set; }
        public int TopAttempt { get; set; }
        public int BonusAttempt { get; set; }

        public ResultsClim()
        {
            TopNumber = 0;
            BonusNumber = 0;
            TopAttempt = 0;
            BonusAttempt = 0;
        }

        public void AddTop(int number)
        {
            TopNumber++;
            TopAttempt += number;
        }

        public void AddBonus(int number)
        {
            BonusNumber++;
            BonusAttempt += number;
        }
    }
}
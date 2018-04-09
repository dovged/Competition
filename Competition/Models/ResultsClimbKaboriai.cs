using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class ResultsClimbKaboriai
    {
        public string Climber { get; set; }
        public int SumPoints { get; set; }
        //PAKEISTI PO TO Į 19
        public int[] RoutePonts = new int[5];
        public int Place { get; set; }

        public ResultsClimbKaboriai()
        {
            SumPoints = 0;
        }

        public void PlusPoints(int points)
        {
            SumPoints += points;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class ResultsClimLBC
    {
        public int Place { get; set; }
        public string Climber { get; set; }
        public int SumAttemptFlash { get; set; }
        public int SumAttemptTop { get; set; }
        public int SumAttemptBonus { get; set; }

        public ResultsClimLBC()
        {
            SumAttemptFlash = 0;
            SumAttemptTop = 0;
            SumAttemptBonus = 0;
        }

        public void PlusFlash()
        {
            SumAttemptFlash++;
        }

        public void PlusTop()
        {
            SumAttemptTop++;
        }

        public void PlusBonus()
        {
            SumAttemptBonus++;
        }

    }
}
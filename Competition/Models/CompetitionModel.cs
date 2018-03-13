using Competition.Context;
using System;
using System.Collections.Generic;

namespace Competition.Models
{
    public class CompetitionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int OrgId { get; set; }
        public int MainRouteCreatorId { get; set; }
        public int MainJudgeId { get; set; }
        public string Club { get; set; }
        //Nustatyti ar laipiojimo varžybos (true), ar KKT varžybos (false);
        public Boolean Type { get; set; }
        public List<TblCompJudge> Judges { get; set; }

        
    }
}